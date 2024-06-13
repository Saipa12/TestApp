using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApp.Data;

namespace TestApp.Services
{
	// Services/SensorDataService.cs

	public class SensorDataService
	{
		private static readonly string[] SensorUrls = {
		"http://178.176.55.245:8084/json",
		"http://178.176.55.245:8085/json",
		"http://178.176.55.245:8086/json"
	};

		public static async Task<List<Data.SensorData>> FetchSensorDataAsync()
		{
			var allData = new List<Data.SensorData>();
			using (HttpClient client = new HttpClient())
			{
				foreach (var url in SensorUrls)
				{
					var response = await client.GetStringAsync(url);
					var data = JsonConvert.DeserializeObject<List<Data.SensorData>>(response);
					allData.AddRange(data);
				}
			}
			return allData;
		}

		public static async Task SaveSensorDataAsync(List<Data.SensorData> data, DateTime date)
		{
			//string databasePath = $"C:\\Users\\Saipa\\Downloads\\SensorData_{date:yyyyMMdd}.db";
			//string databasePath = $"Data Source=SensorData.db";
			string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData.db";
			using (var context = new SensorDataContext(databasePath))
			{
				context.Database.EnsureCreated();
				context.SensorData.AddRange(data);
				await context.SaveChangesAsync();
			}
		}

		public static async Task<List<Data.SensorData>> LoadSensorDataAsync(DateTime date)
		{
			//string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData_{date:yyyyMMdd}.db";
			string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData.db";
			using (var context = new SensorDataContext(databasePath))
			{
				var dataService = new SensorDataService(context);

				await dataService.LoadAndSaveSensorDataAsync("http://178.176.55.245:8084/json", date);
				await dataService.LoadAndSaveSensorDataAsync("http://178.176.55.245:8085/json", date);
				await dataService.LoadAndSaveSensorDataAsync("http://178.176.55.245:8086/json", date);
				return await context.SensorData
									.OrderByDescending(d => d.Packet.Datetime)
									.Take(100)
									.ToListAsync();
			}
		}

		public static void ExportToCsv(List<Data.SensorData> data, string filePath)
		{
			var csv = new StringBuilder();
			csv.AppendLine("Timestamp,Value"); // Добавьте другие поля по необходимости

			foreach (var item in data)
			{
				csv.AppendLine($"{item.Packet.Datetime},{item.Packet.EnvTemperature}");
			}

			File.WriteAllText(filePath, csv.ToString());
		}

		private readonly SensorDataContext _context;

		public SensorDataService(SensorDataContext context)
		{
			_context = context;
		}

		public async Task LoadAndSaveSensorDataAsync(string url, DateTime date)
		{
			try
			{
				// Загрузка данных JSON
				var httpClient = new HttpClient();
				var json = await httpClient.GetStringAsync(url);

				// Преобразование JSON в объект SensorData
				var sensorData = Newtonsoft.Json.JsonConvert.DeserializeObject<SensorData>(json);

				// Если нужно сгенерировать Serial как число, преобразованное в строку
				// Пример: если предыдущий Serial был числом, увеличиваем его на 1
				if (_context.PacketData.Any())
				{
					var lastPacket = _context.PacketData.OrderByDescending(p => p.Serial).FirstOrDefault();
					if (lastPacket != null && long.TryParse(lastPacket.Serial, out long lastSerialNumber))
					{
						sensorData.Packet.Serial = (lastSerialNumber + 1).ToString();
					}
					else
					{
						sensorData.Packet.Serial = "1"; // или другой начальный номер
					}
				}
				else
				{
					sensorData.Packet.Serial = "1"; // или другой начальный номер
				}

				// Сохранение данных в базу данных
				sensorData.Packet.Datetime = date;
				_context.PacketData.Add(sensorData.Packet);
				_context.SensorData.Add(sensorData);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				// Обработка ошибок, если необходимо
				Console.WriteLine($"An error occurred while loading data from {url}: {ex.Message}");
			}
		}
	}
}