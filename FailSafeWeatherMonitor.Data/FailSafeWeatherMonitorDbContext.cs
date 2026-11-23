using FailSafeWeatherMonitor.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FailSafeWeatherMonitor.Data;

public class FailSafeWeatherMonitorDbContext : DbContext
{
    public DbSet<WeatherDataPoint> WeatherDataPoints { get; set; } = null!;

    public FailSafeWeatherMonitorDbContext()
    {
    }

    public FailSafeWeatherMonitorDbContext(DbContextOptions<FailSafeWeatherMonitorDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;"
                + "Database=xxxFailSafeWeatherMonitor;"
                + "Trusted_Connection=True;"
                + "TrustServerCertificate=True;"
            );
        }
    }

}

