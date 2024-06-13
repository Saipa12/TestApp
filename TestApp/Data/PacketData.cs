using System;

namespace TestApp.Data
{
	public class PacketData
	{
		// Для первого примера JSON-данных
		public string? Serial { get; set; }

		public DateTime? Datetime { get; set; }

		public double? EnvTemperature { get; set; }
		public double? Humidity { get; set; }
		public double? DewPoint { get; set; }
		public double? Pressure_hPa { get; set; }
		public double? Pressure_mm_hg { get; set; }
		public double? WindSpeed { get; set; }
		public double? WindDirection { get; set; }
		public double? WindVSound { get; set; }
		public int? PrecipitationType { get; set; }
		public double? PrecipitationIntensity { get; set; }
		public double? PrecipitationQuantity { get; set; }
		public int? PrecipitationElaps { get; set; }
		public int? PrecipitationPeriod { get; set; }
		public double? SupplyVoltage { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public int? Altitude { get; set; }
		public int? KSP { get; set; }
		public double? AccelerationStDev { get; set; }
		public double? Roll { get; set; }
		public double? Pitch { get; set; }
		public int? WeAreFine { get; set; }

		public double? VisibleRange { get; set; }
	}
}