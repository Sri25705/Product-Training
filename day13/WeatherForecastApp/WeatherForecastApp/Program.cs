using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherForecastApp
{
    public class WeatherService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public WeatherService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }
        public async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            try
            {
                string url = $"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                if (data["error"] != null)
                {
                    Console.WriteLine($"Error fetching data{city}");
                    return null;
                }

                if (data["current"] == null || data["location"]==null)
                {
                    Console.WriteLine($"Invalid response{city}");
                    return null;
                }

                return new WeatherInfo
                {
                    City = data["location"]["name"].ToString(),
                    Temperature = data["current"]["temp_c"].ToObject<double>(),
                    Condition = data["current"]["condition"]["text"].ToString(),
                    Humidity = data["current"]["humidity"].ToObject<int>()
                };
            }
            catch (HttpRequestException)
            {
                Console.WriteLine($"Error while fetching data{city}");
                return null;
            }
        }
    }
    public class WeatherInfo
    {
        public required string City { get; set; }
        public double Temperature { get; set;}
        public required string Condition { get; set; }
        public int Humidity { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("enter city name");
            string input = Console.ReadLine();
            string[] cities = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            string apiKey = "e5fe9c3b87164e129a5100948252909";
            WeatherService weatherService = new WeatherService(apiKey);

            List<Task<WeatherInfo>> tasks = new List<Task<WeatherInfo>>();
            foreach(var city in cities)
            {
                tasks.Add(weatherService.GetWeatherAsync(city));
            }

            WeatherInfo[] results = await Task.WhenAll(tasks);

            Console.WriteLine("Weather Forecast:");
            foreach(var weather in results)
            {
                if(weatherService!=null)
                {
                    Console.WriteLine($"City:{weather.City}");
                    Console.WriteLine($"Temperature:{weather.Temperature}");
                    Console.WriteLine($"Condition:{weather.Condition}");
                    Console.WriteLine($"Humidity:{weather.Humidity}%");
                    Console.WriteLine(new string('-', 30));
                }
            }
        }
    }

}