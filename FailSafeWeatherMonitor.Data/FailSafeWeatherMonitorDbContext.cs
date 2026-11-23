using FailSafeWeatherMonitor.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FailSafeWeatherMonitor.Data;

public class FailSafeWeatherMonitorDbContext : DbContext
{
    public DbSet<WeatherDataPoint> WeatherDataPoints { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;"
                + "Database=FailSafeWeatherMonitor;"
                + "Trusted_Connection=True;"
                + "TrustServerCertificate=True;"
            );
        }
    }

}

