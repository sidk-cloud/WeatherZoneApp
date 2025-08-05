using Microsoft.EntityFrameworkCore;
using WeatherZoneApp.Models;

namespace WeatherZoneApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            // Check if there are any cities
            if (context.Cities.Any())
            {
                return;   // DB has been seeded
            }

            var cities = new List<City>
            {
                new City { 
                    Name = "Tokyo", 
                    Country = "Japan", 
                    Population = 37.4, 
                    ImageUrl = "https://images.unsplash.com/photo-1540959733332-eab4deabeeaf?w=500",
                    TimeZone = "UTC+9",
                    Language = "Japanese",
                    Currency = "Japanese Yen (JPY)",
                    GdpPerCapita = 42067,
                    AreaKm2 = 2194,
                    Region = "Kantō",
                    FamousLandmarks = "Tokyo Tower, Senso-ji Temple",
                    PopulationDensity = 6392,
                    IsEnabled = true
                },
                new City { 
                    Name = "Delhi", 
                    Country = "India", 
                    Population = 30.3, 
                    ImageUrl = "https://images.unsplash.com/photo-1587474260584-136574528ed5?w=500",
                    TimeZone = "UTC+5:30",
                    Language = "Hindi, English",
                    Currency = "Indian Rupee (INR)",
                    GdpPerCapita = 6351,
                    AreaKm2 = 1484,
                    Region = "National Capital Territory",
                    FamousLandmarks = "Red Fort, Qutub Minar",
                    PopulationDensity = 11320,
                    IsEnabled = true
                },
                new City { 
                    Name = "Shanghai", 
                    Country = "China", 
                    Population = 27.0, 
                    ImageUrl = "https://images.unsplash.com/photo-1545893835-abaa50cbe628?w=500",
                    TimeZone = "UTC+8",
                    Language = "Mandarin Chinese",
                    Currency = "Chinese Yuan (CNY)",
                    GdpPerCapita = 22467,
                    AreaKm2 = 6341,
                    Region = "Eastern China",
                    FamousLandmarks = "The Bund, Oriental Pearl Tower",
                    PopulationDensity = 3925,
                    IsEnabled = true
                },
                new City { 
                    Name = "São Paulo", 
                    Country = "Brazil", 
                    Population = 22.4, 
                    ImageUrl = "https://images.unsplash.com/photo-1578002573559-689b0abc4148?w=500",
                    TimeZone = "UTC-3",
                    Language = "Portuguese",
                    Currency = "Brazilian Real (BRL)",
                    GdpPerCapita = 15355,
                    AreaKm2 = 1521,
                    Region = "Southeast Brazil",
                    FamousLandmarks = "Paulista Avenue, Ibirapuera Park",
                    PopulationDensity = 7216,
                    IsEnabled = true
                },
                new City { 
                    Name = "Mexico City", 
                    Country = "Mexico", 
                    Population = 22.1, 
                    ImageUrl = "https://images.unsplash.com/photo-1585464231875-d9ef1f5ad396?w=500",
                    TimeZone = "UTC-6",
                    Language = "Spanish",
                    Currency = "Mexican Peso (MXN)",
                    GdpPerCapita = 19796,
                    AreaKm2 = 1485,
                    Region = "Valley of Mexico",
                    FamousLandmarks = "Zócalo, Palacio de Bellas Artes",
                    PopulationDensity = 6000,
                    IsEnabled = true
                },
                new City { 
                    Name = "Cairo", 
                    Country = "Egypt", 
                    Population = 21.8, 
                    ImageUrl = "https://images.unsplash.com/photo-1572252009286-268acec5ca0a?w=500",
                    TimeZone = "UTC+2",
                    Language = "Arabic",
                    Currency = "Egyptian Pound (EGP)",
                    GdpPerCapita = 3624,
                    AreaKm2 = 3085,
                    Region = "Cairo Governorate",
                    FamousLandmarks = "Pyramids of Giza, Egyptian Museum",
                    PopulationDensity = 3100,
                    IsEnabled = true
                },
                new City { 
                    Name = "Mumbai", 
                    Country = "India", 
                    Population = 21.3, 
                    ImageUrl = "https://images.unsplash.com/photo-1566552881560-0be862a7c445?w=500",
                    TimeZone = "UTC+5:30",
                    Language = "Marathi, Hindi",
                    Currency = "Indian Rupee (INR)",
                    GdpPerCapita = 4583,
                    AreaKm2 = 603,
                    Region = "Maharashtra",
                    FamousLandmarks = "Gateway of India, Taj Mahal Palace Hotel",
                    PopulationDensity = 21000,
                    IsEnabled = true
                },
                new City { 
                    Name = "Beijing", 
                    Country = "China", 
                    Population = 20.9, 
                    ImageUrl = "https://images.unsplash.com/photo-1584093091778-e7f4e76e8063?w=500",
                    TimeZone = "UTC+8",
                    Language = "Mandarin Chinese",
                    Currency = "Chinese Yuan (CNY)",
                    GdpPerCapita = 23628,
                    AreaKm2 = 16410,
                    Region = "Northern China",
                    FamousLandmarks = "Great Wall, Forbidden City",
                    PopulationDensity = 1300,
                    IsEnabled = true
                },
                new City { 
                    Name = "Dhaka", 
                    Country = "Bangladesh", 
                    Population = 21.7, 
                    ImageUrl = "https://images.unsplash.com/photo-1583422409516-2895a77efded?w=500",
                    TimeZone = "UTC+6",
                    Language = "Bengali",
                    Currency = "Bangladeshi Taka (BDT)",
                    GdpPerCapita = 2122,
                    AreaKm2 = 306,
                    Region = "Dhaka Division",
                    FamousLandmarks = "Lalbagh Fort, Ahsan Manzil",
                    PopulationDensity = 23234,
                    IsEnabled = true
                },
                new City { 
                    Name = "Osaka", 
                    Country = "Japan", 
                    Population = 19.1, 
                    ImageUrl = "https://images.unsplash.com/photo-1590559899731-a382839e5549?w=500",
                    TimeZone = "UTC+9",
                    Language = "Japanese",
                    Currency = "Japanese Yen (JPY)",
                    GdpPerCapita = 39321,
                    AreaKm2 = 223,
                    Region = "Kansai",
                    FamousLandmarks = "Osaka Castle, Dōtonbori",
                    PopulationDensity = 12030,
                    IsEnabled = true
                },
                new City { 
                    Name = "New York", 
                    Country = "USA", 
                    Population = 18.8, 
                    ImageUrl = "https://images.unsplash.com/photo-1496442226666-8d4d0e62e6e9?w=500",
                    TimeZone = "UTC-5",
                    Language = "English",
                    Currency = "US Dollar (USD)",
                    GdpPerCapita = 75456,
                    AreaKm2 = 784,
                    Region = "Mid-Atlantic",
                    FamousLandmarks = "Statue of Liberty, Empire State Building",
                    PopulationDensity = 10194,
                    IsEnabled = true
                },
                new City { 
                    Name = "Karachi", 
                    Country = "Pakistan", 
                    Population = 16.8, 
                    ImageUrl = "https://images.unsplash.com/photo-1593108408993-58ee9c7825c2?w=500",
                    TimeZone = "UTC+5",
                    Language = "Urdu, Sindhi",
                    Currency = "Pakistani Rupee (PKR)",
                    GdpPerCapita = 1547,
                    AreaKm2 = 3780,
                    Region = "Sindh",
                    FamousLandmarks = "Mohatta Palace, Quaid's Mausoleum",
                    PopulationDensity = 4500,
                    IsEnabled = true
                },
                new City { 
                    Name = "Buenos Aires", 
                    Country = "Argentina", 
                    Population = 15.3, 
                    ImageUrl = "https://images.unsplash.com/photo-1589909202802-8f4aadce1849?w=500",
                    TimeZone = "UTC-3",
                    Language = "Spanish",
                    Currency = "Argentine Peso (ARS)",
                    GdpPerCapita = 19741,
                    AreaKm2 = 203,
                    Region = "Buenos Aires Province",
                    FamousLandmarks = "Casa Rosada, Obelisk",
                    PopulationDensity = 14307,
                    IsEnabled = true
                },
                new City { 
                    Name = "Istanbul", 
                    Country = "Turkey", 
                    Population = 15.2, 
                    ImageUrl = "https://images.unsplash.com/photo-1524231757912-21f4fe3a7200?w=500",
                    TimeZone = "UTC+3",
                    Language = "Turkish",
                    Currency = "Turkish Lira (TRY)",
                    GdpPerCapita = 12888,
                    AreaKm2 = 5461,
                    Region = "Marmara",
                    FamousLandmarks = "Hagia Sophia, Blue Mosque",
                    PopulationDensity = 2759,
                    IsEnabled = true
                },
                new City { 
                    Name = "Kolkata", 
                    Country = "India", 
                    Population = 14.9, 
                    ImageUrl = "https://images.unsplash.com/photo-1558431382-27e303142255?w=500",
                    TimeZone = "UTC+5:30",
                    Language = "Bengali, Hindi",
                    Currency = "Indian Rupee (INR)",
                    GdpPerCapita = 2553,
                    AreaKm2 = 205,
                    Region = "West Bengal",
                    FamousLandmarks = "Victoria Memorial, Howrah Bridge",
                    PopulationDensity = 24252,
                    IsEnabled = true
                },
                new City { 
                    Name = "Manila", 
                    Country = "Philippines", 
                    Population = 14.4, 
                    ImageUrl = "https://images.unsplash.com/photo-1518509562904-e7ef99cdcc86?w=500",
                    TimeZone = "UTC+8",
                    Language = "Filipino, English",
                    Currency = "Philippine Peso (PHP)",
                    GdpPerCapita = 3277,
                    AreaKm2 = 42.88,
                    Region = "National Capital Region",
                    FamousLandmarks = "Intramuros, Rizal Park",
                    PopulationDensity = 42857,
                    IsEnabled = true
                },
                new City { 
                    Name = "Lagos", 
                    Country = "Nigeria", 
                    Population = 14.4, 
                    ImageUrl = "https://images.unsplash.com/photo-1587223075055-82e9a937ddff?w=500",
                    TimeZone = "UTC+1",
                    Language = "English, Yoruba",
                    Currency = "Nigerian Naira (NGN)",
                    GdpPerCapita = 4182,
                    AreaKm2 = 1171,
                    Region = "Lagos State",
                    FamousLandmarks = "National Museum, Lekki Conservation Centre",
                    PopulationDensity = 6871,
                    IsEnabled = true
                },
                new City { 
                    Name = "Rio de Janeiro", 
                    Country = "Brazil", 
                    Population = 13.6, 
                    ImageUrl = "https://images.unsplash.com/photo-1483729558449-99ef09a8c325?w=500",
                    TimeZone = "UTC-3",
                    Language = "Portuguese",
                    Currency = "Brazilian Real (BRL)",
                    GdpPerCapita = 13889,
                    AreaKm2 = 1200,
                    Region = "Southeast Brazil",
                    FamousLandmarks = "Christ the Redeemer, Sugarloaf Mountain",
                    PopulationDensity = 5377,
                    IsEnabled = true
                },
                new City { 
                    Name = "Tianjin", 
                    Country = "China", 
                    Population = 13.6, 
                    ImageUrl = "https://images.unsplash.com/photo-1619873103237-c27faf09a3f2?w=500",
                    TimeZone = "UTC+8",
                    Language = "Mandarin Chinese",
                    Currency = "Chinese Yuan (CNY)",
                    GdpPerCapita = 17253,
                    AreaKm2 = 11760,
                    Region = "Northern China",
                    FamousLandmarks = "Tianjin Eye, Ancient Culture Street",
                    PopulationDensity = 1296,
                    IsEnabled = true
                },
                new City { 
                    Name = "Kinshasa", 
                    Country = "DR Congo", 
                    Population = 13.5, 
                    ImageUrl = "https://images.unsplash.com/photo-1616682780315-66e2a2c6b531?w=500",
                    TimeZone = "UTC+1",
                    Language = "French, Lingala",
                    Currency = "Congolese Franc (CDF)",
                    GdpPerCapita = 557,
                    AreaKm2 = 9965,
                    Region = "Kinshasa Province",
                    FamousLandmarks = "National Museum, Lola ya Bonobo",
                    PopulationDensity = 1355,
                    IsEnabled = true
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
