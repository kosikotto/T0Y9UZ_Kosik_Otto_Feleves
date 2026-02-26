using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public class WeatherService : IWeatherService
    {
        private readonly string API_KEY = "&appid=3787e126ab00325569b990fcc7bf26c9&units=metric";
        private readonly string BASE_API_LOCATION = "https://api.openweathermap.org/data/2.5/forecast?q=";
        private string BASE_API_COORDINATES = "https://api.openweathermap.org/data/2.5/forecast?lat=";
        public Location location;
        public async Task<bool> EnsureLocationPermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            return status == PermissionStatus.Granted;
        }
        public async Task<string> FetchDataAsnyc()
        {
            if (!await EnsureLocationPermissionAsync())
                return null;

            try
            {
                location = await Geolocation.Default.GetLastKnownLocationAsync();

                if (location == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Low);

                    location = await Geolocation.GetLocationAsync(request);
                }

                if (location != null)
                {
                    double latitude = location.Latitude;
                    double longitude = location.Longitude;

                    HttpClient client = new HttpClient();
                    var responseMessage = await client.GetStringAsync($"{BASE_API_COORDINATES}{latitude}&lon={longitude}{API_KEY}");
                    return responseMessage;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public async Task<string> FetchDataAsnyc(string location)
        {
            try
            {
                HttpClient client = new HttpClient();
                var responseMessage = await client.GetStringAsync($"{BASE_API_LOCATION}{location}{API_KEY}");
                return responseMessage;
            }
            catch
            {
                return null;
            }
        }
        public List<object> ParseData(string responseMessage)
        {
            using JsonDocument doc = JsonDocument.Parse(responseMessage);
            JsonElement root = doc.RootElement;

            List<IWeatherForecast> weatherInfos = new List<IWeatherForecast>();
            HashSet<DateTime> uniqueDays = new HashSet<DateTime>();

            IGeoInfo geoInfo = new GeoInfo(
                root.GetProperty("city").GetProperty("country").GetString() ?? "",
                root.GetProperty("city").GetProperty("name").GetString() ?? "",
                DateTime.Now
            );

            DateTime now = DateTime.Now;

            JsonElement list = root.GetProperty("list");
            int count = list.GetArrayLength();

            for (int i = 0; i < count; i++)
            {
                var currentData = list[i];
                var dateString = currentData.GetProperty("dt_txt").GetString();
                var date = DateTime.Parse(dateString);
                DateTime forecastDay = date.Date;

                if (weatherInfos.Count == 0)
                {
                    if (date >= now)
                    {
                        weatherInfos.Add(CreateForecast(currentData));
                        uniqueDays.Add(forecastDay);
                    }
                }
                else
                {
                    if (!uniqueDays.Contains(forecastDay))
                    {
                        if (date.Hour == 12)
                        {
                            weatherInfos.Add(CreateForecast(currentData));
                            uniqueDays.Add(forecastDay);
                        }
                    }
                }

                if (weatherInfos.Count >= 5)
                {
                    break;
                }
            }

            if (weatherInfos.Count == 0 && count > 0)
            {
                weatherInfos.Add(CreateForecast(list[0]));
            }

            return new List<object>() { weatherInfos, geoInfo };
        }
        private WeatherForecast CreateForecast(JsonElement data)
        {
            return new WeatherForecast(
                data.GetProperty("dt_txt").GetString(),
                data.GetProperty("main").GetProperty("temp").GetDouble(),
                data.GetProperty("weather")[0].GetProperty("description").GetString() ?? "",
                data.GetProperty("weather")[0].GetProperty("icon").GetString() ?? "",
                data.GetProperty("weather")[0].GetProperty("icon").GetString() ?? "",
                data.GetProperty("main").GetProperty("humidity").GetInt32(),
                data.GetProperty("wind").GetProperty("speed").GetDouble(),
                data.GetProperty("main").GetProperty("pressure").GetInt32(),
                data.GetProperty("clouds").GetProperty("all").GetInt32()
            );
        }
    }
}