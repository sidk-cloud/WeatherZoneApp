namespace WeatherZoneApp.Models
{
    public class City
    {
        public required string Name { get; set; }
        public required string Country { get; set; }
        public double Population { get; set; }
        public required string ImageUrl { get; set; }
        public WeatherInfo? Weather { get; set; }
        public string? TimeZone { get; set; }
        public string? Language { get; set; }
        public string? Currency { get; set; }
        public double? GdpPerCapita { get; set; }
        public double? AreaKm2 { get; set; }
        public string? Region { get; set; }
        public int? YearFounded { get; set; }
        public string? FamousLandmarks { get; set; }
        public double? PopulationDensity { get; set; }
    }

    public class WeatherInfo
    {
        public double Temperature { get; set; }
        public required string Description { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double? FeelsLike { get; set; }
        public int? Visibility { get; set; }
        public string? Sunrise { get; set; }
        public string? Sunset { get; set; }
    }
}
