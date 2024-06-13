using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestApp.Data;

public class DbContextFactory : IDesignTimeDbContextFactory<SensorDataContext>
{
	public SensorDataContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<SensorDataContext>();
		optionsBuilder.UseSqlite();

		return new SensorDataContext(optionsBuilder.Options);
	}
}