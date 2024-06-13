using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using TestApp.Services;

namespace TestApp.Views
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		private async void LoadData_Click(object sender, RoutedEventArgs e)
		{
			DateTime date = DateTime.Now; // Пример, как получить данные за текущую дату
			var data = await SensorDataService.FetchSensorDataAsync();
			SensorDataGrid.ItemsSource = data.ToList();
		}

		private async void ExportToCsv_Click(object sender, RoutedEventArgs e)
		{
			DateTime date = DateTime.Now; // Пример, как получить данные за текущую дату
			var data = await SensorDataService.FetchSensorDataAsync();

			// Укажите путь к файлу CSV
			string filePath = "C:\\Users\\Saipa\\Downloads\\SensorData.csv";
			SensorDataService.ExportToCsv(data, filePath);
		}
	}
}