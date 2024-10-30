namespace To_Do_API.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherAsync(string location);
    }
}