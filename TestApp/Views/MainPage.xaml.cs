using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using TestApp.Data;
using TestApp.Services;

namespace TestApp.Views
{
	public sealed partial class MainPage : Page
	{
		//private SensorDataViewModel _viewModel;
		private List<SensorData> SensorData;

		public MainPage()
		{
			this.InitializeComponent();
			//_viewModel = DataContext as SensorDataViewModel;
			//this.Loaded += MainPage_Loaded;
		}

		//private async void MainPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
		//{
		//	await LoadSensorDataAsync();
		//}

		//private async Task LoadSensorDataAsync()
		//{
		//	if (_viewModel != null)
		//	{
		//		await _viewModel.LoadSensorDataAsync();
		//	}
		//}

		private async void LoadData_Click(object sender, RoutedEventArgs e)
		{
			DateTime date = DateTime.Now; // Пример, как получить данные за текущую дату
			var data = await SensorDataService.LoadSensorDataAsync(date);
			SensorData = data;
			//var data2 = await PackageDataService.LoadPackageDataAsync(date);
			////PackageDataGrid.ItemsSource = data2;
		}

		private async void ExportToCsv_Click(object sender, RoutedEventArgs e)
		{
			DateTime date = DateTime.Now; // Пример, как получить данные за текущую дату
										  //var data = await SensorDataService.LoadSensorDataAsync(date);

			// Укажите путь к файлу CSV
			string filePath = "C:\\Users\\Saipa\\Downloads\\SensorData.csv";
			//SensorDataService.ExportToCsv(data, filePath);
		}
	}
}