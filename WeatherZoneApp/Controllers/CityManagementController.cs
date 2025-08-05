using Microsoft.AspNetCore.Mvc;
using WeatherZoneApp.Models;
using WeatherZoneApp.Services;

namespace WeatherZoneApp.Controllers
{
    public class CityManagementController : Controller
    {
        private readonly CityService _cityService;
        private readonly CityDataService _cityDataService;

        public CityManagementController(CityService cityService, CityDataService cityDataService)
        {
            _cityService = cityService;
            _cityDataService = cityDataService;
        }

        public async Task<IActionResult> Index()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return View(cities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(City city)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                TempData["Error"] = $"Model validation failed: {errors}";
                return View(city);
            }

            try
            {
                // Get additional city data from external APIs
                var cityData = await _cityDataService.GetCityDataAsync(city.Name, city.Country);
                
                if (cityData == null)
                {
                    TempData["Error"] = "Could not find city data. Please check the city and country names.";
                    ModelState.AddModelError("", "Could not find city data. Please check the city and country names.");
                    return View(city);
                }

                // Use all the retrieved data from cityData
                city.TimeZone = cityData.TimeZone;
                city.Language = cityData.Language;
                city.Currency = cityData.Currency;
                city.ImageUrl = cityData.ImageUrl;
                city.IsEnabled = true;
                city.Population = cityData.Population;
                city.GdpPerCapita = cityData.GdpPerCapita;
                city.AreaKm2 = cityData.AreaKm2;
                city.Region = cityData.Region;
                city.PopulationDensity = cityData.PopulationDensity;
                city.FamousLandmarks = cityData.FamousLandmarks;
                city.YearFounded = cityData.YearFounded;

                try
                {
                    await _cityService.AddCityAsync(city);
                    TempData["Success"] = "City was successfully created.";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException aex)
                {
                    TempData["Error"] = aex.Message;
                    ModelState.AddModelError("", aex.Message);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while creating the city: {ex.Message}";
                ModelState.AddModelError("", $"An error occurred while creating the city: {ex.Message}");
            }
            return View(city);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _cityService.UpdateCityAsync(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _cityService.ToggleCityStatusAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _cityService.DeleteCityAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
