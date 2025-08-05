using Microsoft.EntityFrameworkCore;
using WeatherZoneApp.Data;
using WeatherZoneApp.Models;

namespace WeatherZoneApp.Services
{
    public class CityService
    {
        private readonly ApplicationDbContext _context;
        private readonly WeatherService _weatherService;

        public CityService(ApplicationDbContext context, WeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        public async Task<List<City>> GetEnabledCitiesAsync()
        {
            var cities = await _context.Cities.Where(c => c.IsEnabled).ToListAsync();
            foreach (var city in cities)
            {
                city.Weather = await _weatherService.GetWeatherAsync(city.Name, city.Country);
            }
            return cities;
        }

        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City?> GetCityByIdAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task<City> AddCityAsync(City city)
        {
            // Try to get weather info to validate the city name and country
            var weather = await _weatherService.GetWeatherAsync(city.Name, city.Country);
            if (weather == null)
            {
                throw new ArgumentException("Invalid city/country combination or weather service is unavailable");
            }

            // Add the city to the database
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task UpdateCityAsync(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCityAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ToggleCityStatusAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                city.IsEnabled = !city.IsEnabled;
                await _context.SaveChangesAsync();
            }
        }
    }
}
