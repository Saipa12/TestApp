using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Views
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		private static readonly string[] SensorUrls = {
			"http://178.176.55.245:8084/json",
			"http://178.176.55.245:8085/json",
			"http://178.176.55.245:8086/json"
		};

		private async Task LoadData()
		{
			var data = await SensorDataService.LoadSensorDataAsync(SensorUrls);
			var statistics = GetParameterStatistics(data.ToList());
			SensorDataGrid.ItemsSource = statistics;
		}

		private async void LoadData_Click(object sender, RoutedEventArgs e)
		{
			await LoadData();
		}

		private async void ExportToCsv_Click(object sender, RoutedEventArgs e)
		{
			var savePicker = new Windows.Storage.Pickers.FileSavePicker();

			var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
			WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);

			savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
			savePicker.FileTypeChoices.Add("CSV file", new List<string>() { ".csv" });
			savePicker.SuggestedFileName = "SensorData";

			Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
			if (file != null)
			{
				var data = await SensorDataService.LoadSensorDataAsync(SensorUrls);
				SensorDataService.ExportToCsv(data, file.Path);
			}
		}

		//TODO Automapper из Data в Model
		private List<ParameterStatistic> GetParameterStatistics(List<Data.SensorData> sensorDataList)
		{
			var parameters = new List<ParameterStatistic>
			{
				new ParameterStatistic
				{
					ParameterName = "Температура воздуха, °С",
					Current = sensorDataList.FirstOrDefault()?.Packet.EnvTemperature.ToString(),
					Average = sensorDataList.Where(x=>x.Packet is not null).Average(d => d.Packet.EnvTemperature).ToString(),
					Minimum = sensorDataList.Where(x=>x.Packet is not null).Min(d => d.Packet.EnvTemperature).ToString(),
					Maximum = sensorDataList.Where(x=>x.Packet is not null).Max(d => d.Packet.EnvTemperature).ToString()
				},
				new ParameterStatistic
				{
					ParameterName = "Влажность, %RH",
					Current = sensorDataList.FirstOrDefault()?.Packet.Humidity.ToString(),
					Average = sensorDataList.Where(x=>x.Packet is not null).Average(d => d.Packet.Humidity).ToString(),
					Minimum = sensorDataList.Where(x=>x.Packet is not null).Min(d => d.Packet.Humidity).ToString(),
					Maximum = sensorDataList.Where(x=>x.Packet is not null).Max(d => d.Packet.Humidity).ToString()
				},
				new ParameterStatistic
				{
					ParameterName = "Температура точки росы, °С",
					Current = sensorDataList.FirstOrDefault()?.Packet.DewPoint.ToString(),
					Average = sensorDataList.Where(x=>x.Packet is not null).Average(d => d.Packet.DewPoint).ToString(),
					Minimum = sensorDataList.Where(x=>x.Packet is not null).Min(d => d.Packet.DewPoint).ToString(),
					Maximum = sensorDataList.Where(x=>x.Packet is not null).Max(d => d.Packet.DewPoint).ToString()
				},
				new ParameterStatistic
				{
					ParameterName = "Атмосферное давление, гПа",
					Current = sensorDataList.FirstOrDefault()?.Packet.Pressure_hPa.ToString(),
					Average = sensorDataList.Where(x=>x.Packet is not null).Average(d => d.Packet.Pressure_hPa).ToString(),
					Minimum = sensorDataList.Where(x=>x.Packet is not null).Min(d => d.Packet.Pressure_hPa).ToString(),
					Maximum = sensorDataList.Where(x=>x.Packet is not null).Max(d => d.Packet.Pressure_hPa).ToString()
				},
				new ParameterStatistic
				{
					ParameterName = "Атмосферное давление, мм.рт.ст.",
					Current = sensorDataList.FirstOrDefault()?.Packet.Pressure_mm_hg.ToString(),
					Average = sensorDataList.Where(x=>x.Packet is not null).Average(d => d.Packet.Pressure_mm_hg).ToString(),
					Minimum = sensorDataList.Where(x=>x.Packet is not null).Min(d => d.Packet.Pressure_mm_hg).ToString(),
					Maximum = sensorDataList.Where(x=>x.Packet is not null).Max(d => d.Packet.Pressure_mm_hg).ToString()
				}
			};

			return parameters;
		}
	}
}