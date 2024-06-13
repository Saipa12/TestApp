using System.ComponentModel.DataAnnotations;

namespace TestApp.Data
{
	public class SensorData
	{
		[Key]
		public string? Serial { get; set; }

		public PacketData Packet { get; set; }
	}
}