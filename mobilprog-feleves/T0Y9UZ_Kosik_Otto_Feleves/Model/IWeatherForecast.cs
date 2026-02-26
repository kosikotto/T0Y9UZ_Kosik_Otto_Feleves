namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public interface IWeatherForecast
    {
        int Clouds { get; }
        string Date { get; }
        string Description { get; }
        int Humidity { get; }
        string Icon { get; }
        int Pressure { get; }
        double Temperature { get; }
        string WeatherIcon { get; }
        double WindSpeed { get; }
    }
}