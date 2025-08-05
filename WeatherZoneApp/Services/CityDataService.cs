using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using WeatherZoneApp.Models;

namespace WeatherZoneApp.Services
{
    public class CityDataService
    {
        private readonly HttpClient _httpClient;

        public CityDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<City?> GetCityDataAsync(string cityName, string country)
        {
            try
            {
                // We only need country data for now, so let's skip the geocoding API since we haven't set up the key
                /* Commenting out geocoding for now as we don't have the API key
                var geoResponse = await _httpClient.GetAsync($"http://api.openweathermap.org/geo/1.0/direct?q={cityName},{country}&limit=1&appid={_geoApiKey}");
                
                if (!geoResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to connect to geocoding service.");
                }

                var geoData = await JsonSerializer.DeserializeAsync<JsonElement[]>(
                    await geoResponse.Content.ReadAsStreamAsync());

                if (geoData == null || geoData.Length == 0)
                {
                    throw new Exception("City not found in geocoding service.");
                }
                */

                // For timezone, language, currency, etc., we could use a free API like REST Countries
                var countryResponse = await _httpClient.GetAsync($"https://restcountries.com/v3.1/name/{country}");
                
                if (!countryResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"Country '{country}' not found. Please check the spelling.");
                }

                var countryData = await JsonSerializer.DeserializeAsync<JsonElement[]>(
                    await countryResponse.Content.ReadAsStreamAsync());

                if (countryData == null || countryData.Length == 0)
                {
                    throw new Exception($"No data found for country '{country}'.");
                }

                // Get the timezone - use TryGetProperty to handle potential missing data
                string? timezone = null;
                // Get timezone from the country data first
                if (countryData[0].TryGetProperty("timezones", out var timezonesElement))
                {
                    timezone = timezonesElement.EnumerateArray().FirstOrDefault().GetString();
                }

                // Get currency information - handle potential missing data
                string currencyInfo = "Unknown";
                if (countryData[0].TryGetProperty("currencies", out var currenciesElement))
                {
                    var currencyObj = currenciesElement.EnumerateObject().FirstOrDefault();
                    if (currencyObj.Value.TryGetProperty("name", out var nameElement))
                    {
                        currencyInfo = $"{nameElement.GetString()} ({currencyObj.Name})";
                    }
                }

                // Get language information - handle potential missing data
                string languageInfo = "Unknown";
                if (countryData[0].TryGetProperty("languages", out var languagesElement))
                {
                    var languageObj = languagesElement.EnumerateObject().FirstOrDefault();
                    if (languageObj.Value.ValueKind != JsonValueKind.Undefined)
                    {
                        languageInfo = languageObj.Value.GetString() ?? "Unknown";
                    }
                }

                // For population and other city-specific data, you might need additional APIs
                // This is a simplified example

                // Set default values
                double population = 1.0; // Default to 1 million
                double gdpPerCapita = 10000; // Default GDP
                double areaKm2 = 100.0; // Default area

                try
                {
                    // Safely parse population
                    if (countryData[0].TryGetProperty("population", out var countryPop))
                    {
                        var countryPopulation = countryPop.GetDouble();
                        // For major cities, estimate 2-15% of country population depending on country size
                        var populationFactor = countryPopulation switch
                        {
                            > 100_000_000 => 0.03, // Large countries like USA, China - major cities are smaller %
                            > 50_000_000 => 0.05,  // Medium-large countries
                            > 20_000_000 => 0.08,  // Medium countries like Australia
                            _ => 0.12               // Smaller countries - capital cities are larger %
                        };
                        population = Math.Round(countryPopulation * populationFactor / 1_000_000, 2); // Convert to millions
                    }

                    // Safely parse area - more realistic city area calculations
                    if (countryData[0].TryGetProperty("area", out var countryArea))
                    {
                        var countryAreaKm2 = countryArea.GetDouble();
                        // City area based on population size
                        areaKm2 = population switch
                        {
                            > 10 => Math.Round(countryAreaKm2 * 0.008, 2), // Mega cities
                            > 5 => Math.Round(countryAreaKm2 * 0.005, 2),  // Large cities
                            > 2 => Math.Round(countryAreaKm2 * 0.003, 2),  // Medium cities
                            _ => Math.Round(countryAreaKm2 * 0.001, 2)     // Smaller cities
                        };
                        
                        // Ensure reasonable minimum and maximum bounds
                        areaKm2 = Math.Max(50, Math.Min(areaKm2, 15000)); 
                    }

                    // Set a reasonable GDP per capita based on region
                    var region = countryData[0].TryGetProperty("region", out var regionElement) 
                        ? regionElement.GetString() 
                        : "Unknown";
                    
                    // Base GDP on region with more realistic estimates
                    gdpPerCapita = region switch
                    {
                        "Europe" => 52000,    // Higher for European cities
                        "Americas" => 48000,  // North/South America average
                        "Asia" => 32000,      // Asian cities (wide range)
                        "Oceania" => 58000,   // Australia/NZ are wealthy
                        "Africa" => 18000,    // African cities
                        _ => 28000            // Global average
                    };

                    // Adjust GDP based on country development level if we can determine it
                    if (countryData[0].TryGetProperty("unMember", out var unMember) && unMember.GetBoolean())
                    {
                        // UN members tend to have more stable economies, slight bump
                        gdpPerCapita = (int)(gdpPerCapita * 1.05);
                    }
                }
                catch
                {
                    // If any calculation fails, keep the default values
                }

                // Generate image URL using city and country for better results
                string imageUrl = $"https://source.unsplash.com/featured/?{Uri.EscapeDataString(cityName)}+{Uri.EscapeDataString(country)}+cityscape";

                return new City
                {
                    Name = cityName,
                    Country = country,
                    TimeZone = timezone ?? "Unknown",
                    Language = languageInfo,
                    Currency = currencyInfo,
                    IsEnabled = true,
                    ImageUrl = imageUrl,
                    Population = population,
                    GdpPerCapita = gdpPerCapita,
                    AreaKm2 = areaKm2,
                    Region = countryData[0].TryGetProperty("region", out var regionData) ? regionData.GetString() : "Unknown",
                    PopulationDensity = Math.Round(population * 1_000_000 / areaKm2, 2), // Calculate based on population and area
                    FamousLandmarks = $"Historic sites and cultural landmarks of {cityName}",
                    YearFounded = 0 // We'll need a separate API or database for historical data
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting city data: {ex.Message}");
            }
        }
    }
}
