using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Models;
using TestApp.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestApp
{
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
			// Устанавливаем начальную страницу
			MainFrame.Navigate(typeof(MainPage));
		}

		private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		public static List<ParameterStatistic> GetParameterStatistics(List<SensorData> sensorDataList)
		{
			var parameters = new List<ParameterStatistic>
	{
		new ParameterStatistic
		{
			ParameterName = "Температура воздуха, °С",
			Current = sensorDataList.LastOrDefault()?.Packet.EnvTemperature.ToString(),
			Average = sensorDataList.Average(d => d.Packet.EnvTemperature).ToString(),
			Minimum = sensorDataList.Min(d => d.Packet.EnvTemperature).ToString(),
			Maximum = sensorDataList.Max(d => d.Packet.EnvTemperature).ToString()
		},
		new ParameterStatistic
		{
			ParameterName = "Влажность, %RH",
			Current = sensorDataList.LastOrDefault()?.Packet.Humidity.ToString(),
			Average = sensorDataList.Average(d => d.Packet.Humidity).ToString(),
			Minimum = sensorDataList.Min(d => d.Packet.Humidity).ToString(),
			Maximum = sensorDataList.Max(d => d.Packet.Humidity).ToString()
		},
		new ParameterStatistic
		{
			ParameterName = "Температура точки росы, °С",
			Current = sensorDataList.LastOrDefault()?.Packet.DewPoint.ToString(),
			Average = sensorDataList.Average(d => d.Packet.DewPoint).ToString(),
			Minimum = sensorDataList.Min(d => d.Packet.DewPoint).ToString(),
			Maximum = sensorDataList.Max(d => d.Packet.DewPoint).ToString()
		},
		new ParameterStatistic
		{
			ParameterName = "Атмосферное давление, гПа",
			Current = sensorDataList.LastOrDefault()?.Packet.Pressure_hPa.ToString(),
			Average = sensorDataList.Average(d => d.Packet.Pressure_hPa).ToString(),
			Minimum = sensorDataList.Min(d => d.Packet.Pressure_hPa).ToString(),
			Maximum = sensorDataList.Max(d => d.Packet.Pressure_hPa).ToString()
		},
		new ParameterStatistic
		{
			ParameterName = "Атмосферное давление, мм.рт.ст.",
			Current = sensorDataList.LastOrDefault()?.Packet.Pressure_mm_hg.ToString(),
			Average = sensorDataList.Average(d => d.Packet.Pressure_mm_hg).ToString(),
			Minimum = sensorDataList.Min(d => d.Packet.Pressure_mm_hg).ToString(),
			Maximum = sensorDataList.Max(d => d.Packet.Pressure_mm_hg).ToString()
		},
        // Добавьте другие параметры аналогично
    };

			return parameters;
		}
	}
}