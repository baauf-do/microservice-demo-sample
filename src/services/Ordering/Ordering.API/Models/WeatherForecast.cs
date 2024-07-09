
namespace Ordering.API.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public int TemperatureC { get; set; } = int.MinValue;

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; } = string.Empty;
    }
}
