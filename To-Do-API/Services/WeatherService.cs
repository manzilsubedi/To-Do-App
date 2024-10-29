using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace To_Do_API.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherInfo> GetWeatherAsync(string location)
        {
            var apiKey = "5994f00de8a34dc090b225759242910"; // Replace with your actual API key

            try
            {
                var response = await _httpClient.GetStringAsync($"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={location}&aqi=no");
                var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(response);

                if (weatherData?.Current != null)
                {
                    return new WeatherInfo
                    {
                        Location = weatherData.Location.Name, // Get location name from the API response
                        Temperature = weatherData.Current.TempC,
                        Condition = weatherData.Current.Condition.Text,
                        Latitude = weatherData.Location.Latitude, // Assuming Location has Latitude
                        Longitude = weatherData.Location.Longitude
                    };
                }
                else
                {
                    // Handle case when weather data is null or invalid
                    throw new Exception("Weather data could not be retrieved.");
                }
            }
            catch (HttpRequestException e)
            {
                // Handle network errors
                throw new Exception("Network error occurred while fetching weather data.", e);
            }
            catch (JsonException e)
            {
                // Handle JSON deserialization errors
                throw new Exception("Error parsing weather data response.", e);
            }
            catch (Exception e)
            {
                // Handle any other exceptions
                throw new Exception("An error occurred while fetching weather data.", e);
            }
        }
    }

    public class WeatherInfo
    {
        public float Temperature { get; set; }
        public string Condition { get; set; }
        public string Location { get; internal set; }
        public double Latitude { get; internal set; }
        public double Longitude { get; internal set; }
    }

    public class WeatherResponse
    {
        public CurrentWeather Current { get; set; }
        public LocationInfo Location { get; set; }
    }

    public class LocationInfo
    {
        public string Name { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }

    public class CurrentWeather
    {
        [JsonProperty("temp_c")]
        public float TempC { get; set; }
        public WeatherCondition Condition { get; set; }
    }

    public class WeatherCondition
    {
        public string Text { get; set; }
    }
}
