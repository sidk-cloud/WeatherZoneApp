using Microsoft.AspNetCore.Mvc;
using WeatherZoneApp.Models;
using WeatherZoneApp.Services;
using WeatherZoneApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherZoneApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherService _weatherService;
        private readonly ApplicationDbContext _context;

        public HomeController(WeatherService weatherService, ApplicationDbContext context)
        {
            _weatherService = weatherService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cities = _context.Cities.Where(c => c.IsEnabled).Take(20).ToList();
            // Add debug information to ViewData
            ViewData["LayoutPath"] = "_Layout";
            ViewData["Debug"] = "Controller action executed";
            return View(cities);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string cityName, string country)
        {
            var weather = await _weatherService.GetWeatherAsync(cityName, country);
            return Json(weather);
        }


    }
}
