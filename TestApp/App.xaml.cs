using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using System;
using TestApp.Data;

namespace TestApp
{
	public partial class App : Application
	{
		public static Window MainWindow { get; private set; }

		public App()
		{
			this.InitializeComponent();
			InitializeDatabase();
		}

		private void InitializeDatabase()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			//	string databasePath = Path.Combine(folderPath, "sensor_data.db");
			DateTime date = DateTime.Now;
			string databasePath = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData_{date:yyyyMMdd}.db";
			//string connectionString = $"Data Source=C:\\Users\\Saipa\\Downloads\\SensorData.db";
			//string connectionString = $"Data Source={databasePath}";

			//if (!File.Exists(databasePath))
			//{
			using (var context = new SensorDataContext(databasePath))
			{
				// Применение миграций только если база данных не существует
				context.Database.Migrate();
			}
			//}
		}

		protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
		{
			m_window = new MainWindow();
			m_window.Activate();
			MainWindow = new MainWindow();
			//MainWindow.Activate();
		}

		private Window m_window;
	}
}