namespace TestApp.Models
{
	public class SensorData
	{
		public string? Serial { get; set; }

		public PacketData Packet { get; set; }
		// Добавьте другие поля, если они есть в JSON
	}
}