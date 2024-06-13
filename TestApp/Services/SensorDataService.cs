using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApp.Data;

namespace TestApp.Services
{
	public static class SensorDataService
	{
		private static readonly string[] SensorUrls = {
			"http://178.176.55.245:8084/json",
			"http://178.176.55.245:8085/json",
			"http://178.176.55.245:8086/json"
		};

		public static async Task<List<SensorData>> FetchSensorDataAsync()
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
				csv.AppendLine($"{item.Serial},{item.Packet.Datetime},{item.Packet.EnvTemperature},{item.Packet.Humidity},{item.Packet.DewPoint},{item.Packet.Pressure_hPa},{item.Packet.Pressure_mm_hg},{item.Packet.WindSpeed},{item.Packet.WindDirection}");
			}

			File.WriteAllText(filePath, csv.ToString());
		}
	}
}