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
	// Services/PackageDataService.cs

	public class PackageDataService
	{
		private static readonly string[] PackageUrls = {
		"http://178.176.55.245:8084/json",
		"http://178.176.55.245:8085/json",
		"http://178.176.55.245:8086/json"
	};

		public static async Task<List<Data.PacketData>> FetchPackageDataAsync()
		{
			var allData = new List<Data.PacketData>();
			using (HttpClient client = new HttpClient())
			{
				foreach (var url in PackageUrls)
				{
					var response = await client.GetStringAsync(url);
					var data = JsonConvert.DeserializeObject<List<Data.PacketData>>(response);
					allData.AddRange(data);
				}
			}
			return allData;
		}

		public static async Task SavePackageDataAsync(List<Data.PacketData> data, DateTime date)
		{
			//string databasePath = $"C:\\Users\\Saipa\\Downloads\\PackageData_{date:yyyyMMdd}.db";
			//string databasePath = $"Data Source=PackageData.db";
			string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData.db";
			using (var context = new SensorDataContext(databasePath))
			{
				context.Database.EnsureCreated();
				context.PacketData.AddRange(data);
				await context.SaveChangesAsync();
			}
		}

		public static async Task<List<Data.PacketData>> LoadPackageDataAsync(DateTime date)
		{
			//string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\PackageData_{date:yyyyMMdd}.db";
			string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData.db";
			using (var context = new SensorDataContext(databasePath))
			{
				var dataService = new PackageDataService(context);

				//await dataService.LoadAndSavePackageDataAsync("http://178.176.55.245:8084/json", date);
				//await dataService.LoadAndSavePackageDataAsync("http://178.176.55.245:8085/json", date);
				//await dataService.LoadAndSavePackageDataAsync("http://178.176.55.245:8086/json", date);
				return await context.PacketData
									.OrderByDescending(d => d.Datetime)
									.Take(100)
									.ToListAsync();
			}
		}

		public static void ExportToCsv(List<Data.PacketData> data, string filePath)
		{
			var csv = new StringBuilder();
			csv.AppendLine("Timestamp,Value"); // Добавьте другие поля по необходимости

			foreach (var item in data)
			{
				csv.AppendLine($"{item.Datetime},{item.EnvTemperature}");
			}

			File.WriteAllText(filePath, csv.ToString());
		}

		private readonly SensorDataContext _context;

		public PackageDataService(SensorDataContext context)
		{
			_context = context;
		}

		////public async Task LoadAndSavePackageDataAsync(string url, DateTime date)
		//{
		//	try
		//	{
		//		// Загрузка данных JSON
		//		var httpClient = new HttpClient();
		//		var json = await httpClient.GetStringAsync(url);

		//		// Преобразование JSON в объект PackageData
		//		var PackageData = Newtonsoft.Json.JsonConvert.DeserializeObject<PackageData>(json);

		//		// Если нужно сгенерировать Serial как число, преобразованное в строку
		//		// Пример: если предыдущий Serial был числом, увеличиваем его на 1
		//		if (_context.PacketData.Any())
		//		{
		//			var lastPacket = _context.PacketData.OrderByDescending(p => p.Serial).FirstOrDefault();
		//			if (lastPacket != null && long.TryParse(lastPacket.Serial, out long lastSerialNumber))
		//			{
		//				PackageData.Packet.Serial = (lastSerialNumber + 1).ToString();
		//			}
		//			else
		//			{
		//				PackageData.Packet.Serial = "1"; // или другой начальный номер
		//			}
		//		}
		//		else
		//		{
		//			PackageData.Packet.Serial = "1"; // или другой начальный номер
		//		}

		//		// Сохранение данных в базу данных
		//		PackageData.Packet.Datetime = date;
		//		_context.PacketData.Add(PackageData.Packet);
		//		_context.PackageData.Add(PackageData);
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (Exception ex)
		//	{
		//		// Обработка ошибок, если необходимо
		//		Console.WriteLine($"An error occurred while loading data from {url}: {ex.Message}");
		//	}
		//}
	}
}