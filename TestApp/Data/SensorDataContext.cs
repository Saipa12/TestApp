using Microsoft.EntityFrameworkCore;

namespace TestApp.Data
{
	public class SensorDataContext : DbContext
	{
		private readonly string _connectionString;

		public SensorDataContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public SensorDataContext()
		{
		}

		public SensorDataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<SensorData> SensorData { get; set; }
		public DbSet<PacketData> PacketData { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite(_connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SensorData>()
					.HasKey(s => s.Serial);

			modelBuilder.Entity<PacketData>()
				.HasKey(p => p.Serial);

			//modelBuilder.Entity<SensorData>()
			//	.OwnsOne(s => s.Packet.Serial);
			//modelBuilder.Entity<SensorData>()
			//   .OwnsOne(s => s.Packet);
		}
	}
}