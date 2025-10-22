using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public class WeatherForecast
    {
        public string Date { get; private set; }
        public double Temperature { get; private set; }
        public string Description { get; private set; }
        public string Icon { get; private set; }
        public string WeatherIcon { get; private set; }
        public int Humidity { get; private set; }
        public double WindSpeed { get; private set; }
        public int Pressure { get; private set; }
        public int Clouds { get; private set; }

        public WeatherForecast(string date, double temperature, string description, string icon, string weatherIcon, int humidity, double windSpeed, int pressure, int clouds)
        {
            Date = date;
            Temperature = temperature;
            Description = description;
            Icon = icon;
            WeatherIcon = weatherIcon;
            Humidity = humidity;
            WindSpeed = windSpeed;
            Pressure = pressure;
            Clouds = clouds;
        }
    }
}
