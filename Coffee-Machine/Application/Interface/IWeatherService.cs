namespace Coffee_Machine.Application.Interface
{
    public interface IWeatherService
    {
        Task<double> GetCurrentTemperatureAsync();
    }
}
