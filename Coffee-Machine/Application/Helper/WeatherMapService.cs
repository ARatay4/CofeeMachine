using Coffee_Machine.Application.Common;
using Coffee_Machine.Application.Interface;

namespace Coffee_Machine.Application.Helper
{
    public class WeatherMapService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherMapService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenWeather:ApiKey"]; ;
        }

        public async Task<double> GetCurrentTemperatureAsync()
        {
            // 1. Get public IP
            var ip = await _httpClient.GetStringAsync("https://api.ipify.org");

            // 2. Get location from IP using free IP API
            var geo = await _httpClient.GetFromJsonAsync<IpGeoResponse>(
                $"https://ipapi.co/{ip}/json/");

            var lat = geo.Latitude;
            var lon = geo.Longitude;

            // OpenWeatherMap legacy endpoint to auto-detect location
            var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(
                $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={_apiKey}");

            return response?.Main.Temp ?? 0;
        }
    }
}
