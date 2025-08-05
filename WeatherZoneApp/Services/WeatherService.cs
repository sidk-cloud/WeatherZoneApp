using WeatherZoneApp.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherZoneApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "57ff85844069222aa311486f58b1c12b"; // Replace with actual OpenWeatherMap API key

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherInfo?> GetWeatherAsync(string cityName, string country)
        {
            var response = await _httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={cityName},{country}&appid={_apiKey}&units=metric");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                var mainData = json["main"];
                var weatherData = json["weather"]?.FirstOrDefault();
                var sysData = json["sys"];

                return new WeatherInfo
                {
                    Temperature = mainData?["temp"]?.Value<double>() ?? 0,
                    Description = weatherData?["description"]?.Value<string>() ?? "No description available",
                    Humidity = mainData?["humidity"]?.Value<int>() ?? 0,
                    WindSpeed = json["wind"]?["speed"]?.Value<double>() ?? 0,
                    FeelsLike = mainData?["feels_like"]?.Value<double>(),
                    Visibility = json["visibility"]?.Value<int>(),
                    Sunrise = sysData?["sunrise"]?.Value<long>() is long sunriseTime
                        ? DateTimeOffset.FromUnixTimeSeconds(sunriseTime)
                            .LocalDateTime.ToString("HH:mm")
                        : null,
                    Sunset = sysData?["sunset"]?.Value<long>() is long sunsetTime
                        ? DateTimeOffset.FromUnixTimeSeconds(sunsetTime)
                            .LocalDateTime.ToString("HH:mm")
                        : null
                };
            }

            return null;
        }
    }
}
