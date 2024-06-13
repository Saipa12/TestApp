using System;

namespace TestApp.Models
{
	public class SensorData
	{
		public DateTime Timestamp { get; set; }
		public double Value { get; set; }
		// Добавьте другие поля, если они есть в JSON
	}
}