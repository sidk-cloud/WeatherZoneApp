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

        public async Task<WeatherInfo> GetWeatherForCity(string cityName)
        {
            var response = await _httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}&units=metric");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                return new WeatherInfo
                {
                    Temperature = json["main"]["temp"].Value<double>(),
                    Description = json["weather"][0]["description"].Value<string>(),
                    Humidity = json["main"]["humidity"].Value<int>(),
                    WindSpeed = json["wind"]["speed"].Value<double>()
                };
            }

            return null;
        }
    }
}
