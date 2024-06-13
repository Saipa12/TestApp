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
	public class SensorDataService
	{
		public static async Task<List<SensorData>> FetchSensorDataAsync(string[] SensorUrls)
		{
			var allData = new List<SensorData>();
			using (HttpClient client = new HttpClient())
			{
				foreach (var url in SensorUrls)
				{
					var response = await client.GetStringAsync(url);
					var data = JsonConvert.DeserializeObject<SensorData>(response);
					data.Packet.Datetime = DateTime.Now;
					allData.Add(data);
				}
			}
			return allData;
		}

		public static void ExportToCsv(List<SensorData> data, string filePath)
		{
			var csv = new StringBuilder();
			csv.AppendLine("Serial,Datetime,EnvTemperature,Humidity,DewPoint,Pressure_hPa,Pressure_mm_hg,WindSpeed,WindDirection"); // Добавьте другие поля по необходимости

			foreach (var item in data)
			{
				var serial = item.Serial ?? string.Empty;
				var datetime = item.Packet?.Datetime.ToString() ?? string.Empty;
				var envTemperature = item.Packet?.EnvTemperature.ToString() ?? string.Empty;
				var humidity = item.Packet?.Humidity.ToString() ?? string.Empty;
				var dewPoint = item.Packet?.DewPoint.ToString() ?? string.Empty;
				var pressure_hPa = item.Packet?.Pressure_hPa.ToString() ?? string.Empty;
				var pressure_mm_hg = item.Packet?.Pressure_mm_hg.ToString() ?? string.Empty;
				var windSpeed = item.Packet?.WindSpeed.ToString() ?? string.Empty;
				var windDirection = item.Packet?.WindDirection.ToString() ?? string.Empty;

				csv.AppendLine($"{serial},{datetime},{envTemperature},{humidity},{dewPoint},{pressure_hPa},{pressure_mm_hg},{windSpeed},{windDirection}");
			}

			File.WriteAllText(filePath, csv.ToString());
		}

		public static async Task<List<Data.SensorData>> LoadSensorDataAsync(string[] SensorUrls)
		{
			DateTime date = DateTime.Now;
			string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData_{date:yyyyMMdd}.db";
			//	string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData.db";
			using (var context = new SensorDataContext(databasePath))
			{
				var dataService = new SensorDataService(context);
				foreach (var sensor in SensorUrls)
				{
					await dataService.LoadAndSaveSensorDataAsync(sensor, date);
				}
				//await dataService.LoadAndSaveSensorDataAsync("http://178.176.55.245:8084/json", date);
				//await dataService.LoadAndSaveSensorDataAsync("http://178.176.55.245:8085/json", date);
				//await dataService.LoadAndSaveSensorDataAsync("http://178.176.55.245:8086/json", date);
				return await context.SensorData
									.OrderByDescending(d => d.Packet.Datetime)
									.Take(100)
									.ToListAsync();
			}
		}

		//public static void ExportToCsv(List<Data.SensorData> data, string filePath)
		//{
		//	var csv = new StringBuilder();
		//	csv.AppendLine("Timestamp,Value"); // Добавьте другие поля по необходимости

		//	foreach (var item in data)
		//	{
		//		csv.AppendLine($"{item.Packet.Datetime},{item.Packet.EnvTemperature}");
		//	}

		//	File.WriteAllText(filePath, csv.ToString());
		//}

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