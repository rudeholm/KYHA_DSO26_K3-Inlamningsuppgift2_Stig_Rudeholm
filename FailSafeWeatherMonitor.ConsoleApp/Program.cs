using FailSafeWeatherMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>()
    .Build();

var options = new DbContextOptionsBuilder<FailSafeWeatherMonitorDbContext>()
    .UseSqlServer(config.GetConnectionString("DefaultConnection"))
    .Options;

using var _db = new FailSafeWeatherMonitorDbContext(options);

var dataPoint = _db.WeatherDataPoints.FirstOrDefault();

if (dataPoint is not null)
{
    Console.WriteLine($"{dataPoint.StationName}");
}
