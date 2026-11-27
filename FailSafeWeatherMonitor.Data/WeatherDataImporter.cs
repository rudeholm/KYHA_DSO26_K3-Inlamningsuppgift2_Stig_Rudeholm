using FailSafeWeatherMonitor.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FailSafeWeatherMonitor.Data;

public class WeatherDataImporter(FailSafeWeatherMonitorDbContext _db)
{
    public void ImportFromFileCSV(string filePath)
    {
        var rawLines = File.ReadAllLines(filePath);
        var badData = new List<string>();
        var validDataPoints = new List<WeatherDataPoint>();
        var seenInFile = new HashSet<(string, DateTime)>();
        var duplicates = new List<string>();

        var existingDataPoints = _db.WeatherDataPoints
            .Select(d => new { d.StationName, d.Timestamp })
            .ToHashSet();

        for (int i = 1; i < rawLines.Length; i++)
        {
            if (rawLines[i].Contains(','))
            {
                badData.Add(rawLines[i]);
                continue;
            }

            var parts = rawLines[i].Split('\t');

            if (parts.Length < 4)
            {
                badData.Add(rawLines[i]);
                continue;
            }

            string StationName = parts[0].Trim();
            string timestampString = parts[1].Trim();
            string temperatureString = parts[2].Trim();
            string humidityString = parts[3].Trim();


            if (string.IsNullOrWhiteSpace(StationName)||string.IsNullOrWhiteSpace(humidityString))
            {
                badData.Add(rawLines[i]);
                continue;
            }

            if (DateTime.TryParse(timestampString, out DateTime Timestamp) is false)
            {
                badData.Add(rawLines[i]);
                continue;
            }

            if (existingDataPoints.Contains(new { StationName, Timestamp })
                || seenInFile.Add((StationName, Timestamp)) is false)
            {
                duplicates.Add(rawLines[i]);
                // TODO: Log duplicate!
                continue;
            }

            if (double.TryParse(temperatureString, NumberStyles.Number, CultureInfo.InvariantCulture, out double Temperature) is false)
            {
                badData.Add(rawLines[i]);
                continue;
            }

            if (Temperature < -60.0 || Temperature > 100.0)
            {
                badData.Add(rawLines[i]);
                continue;
            }

            if (double.TryParse(humidityString, NumberStyles.Number, CultureInfo.InvariantCulture, out double Humidity) is false)
            {
                badData.Add(rawLines[i]);
                continue;
            }

            if (Humidity < 0.0 || Humidity > 100.0)
            {
                badData.Add(rawLines[i]);
                continue;
            }

            validDataPoints.Add(new WeatherDataPoint()
            {
                StationName = StationName,
                Timestamp = Timestamp,
                Temperature = Temperature,
                Humidity = Humidity,
                DataSource = "CSV-File"
            });
        }

        //_db.WeatherDataPoints.AddRange(validDataPoints);
        //_db.SaveChanges();

        Console.WriteLine($"Number of valid rows imported: {validDataPoints.Count}");
        Console.WriteLine($"Duplicates: {duplicates.Count}");

        foreach (var item in badData)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("###########");
        foreach (var item in duplicates)
        {
            Console.WriteLine(item);
        }

    }
}

/*

        _db.Vehicles.AddRange(validVehicles);
        _db.SaveChanges();

        Console.WriteLine($"Import klar! Godkända: {validVehicles.Count} | Avvisade: {errors.Count}");
        foreach (var err in errors)
        {
            Console.WriteLine($"  - {err}");
        }
    }
}

*/