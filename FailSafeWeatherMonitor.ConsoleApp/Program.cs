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

//var dataPoint = _db.WeatherDataPoints
//    .Where(w => w.Id == 1)
//    .FirstOrDefault();

//if (dataPoint is not null)
//{
//    Console.WriteLine($"{dataPoint.StationName}");
//    _db.WeatherDataPoints.Remove(dataPoint);
//    _db.SaveChanges();
//}
//else
//{
//    Console.WriteLine("not found");
//}

Console.BackgroundColor = ConsoleColor.Black;
Console.Clear();
Console.CursorVisible = false;


var foo = new WeatherDataImporter(_db);
Console.WriteLine("--------------------------------------------");
foo.ImportFromFileCSV("weather_data.csv");
Console.WriteLine("--------------------------------------------");
foo.ImportFromFileCSV("weather_data2.csv");


Console.CursorVisible = false;
Console.ResetColor();
Console.WriteLine("\n\n");

