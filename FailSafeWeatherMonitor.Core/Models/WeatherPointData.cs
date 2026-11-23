namespace FailSafeWeatherMonitor.Core.Models;

public class WeatherPointData
{
    public int Id { get; set; }
    
    public string StationName { get; set; }

    public DateTime Timestamp { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }

    public string DataSource { get; set; }
}
