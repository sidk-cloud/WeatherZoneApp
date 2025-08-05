using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherZoneApp.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        public double Population { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        [NotMapped]
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
        public bool IsEnabled { get; set; } = true;
    }

    public class WeatherInfo
    {
        public double Temperature { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double? FeelsLike { get; set; }
        public int? Visibility { get; set; }
        public string? Sunrise { get; set; }
        public string? Sunset { get; set; }
    }
}
