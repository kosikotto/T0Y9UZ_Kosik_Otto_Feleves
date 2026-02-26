
namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public interface ISharedDataService
    {
        IGeoInfo? CurrentGeoInfo { get; set; }
        List<IWeatherForecast>? CurrentWeatherForecasts { get; set; }
    }
}