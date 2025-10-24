using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public class WeatherService
    {
        private static readonly string API_KEY = "&appid=3787e126ab00325569b990fcc7bf26c9&units=metric";
        private static readonly string BASE_API_LOCATION = "https://api.openweathermap.org/data/2.5/forecast?q=";
        private static readonly string BASE_API_COORDINATES = "https://api.openweathermap.org/data/2.5/forecast?lat=";
        public static Location location;

        public static async Task<string> FetchData()
        {
            try
            {
                location = await Geolocation.Default.GetLastKnownLocationAsync();

                if(location != null)
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

        public static async Task<string> FetchData(string location)
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
        public static async Task<List<object>> ParseData(string responseMessage)
        {
            using JsonDocument doc = JsonDocument.Parse(responseMessage);
            JsonElement root = doc.RootElement;

            List<WeatherForecast> weatherInfos = new List<WeatherForecast>();
            GeoInfo geoInfo = new GeoInfo(
                root.GetProperty("city").GetProperty("country").GetString() ?? "", //Country
                root.GetProperty("city").GetProperty("name").GetString() ?? "", //CityName
                DateTime.Now //CurrentDate
                );

            for (int i = 0; i < root.GetProperty("list").GetArrayLength(); i++)
            {
                var currentData = root.GetProperty("list")[i];
                var date = DateTime.Parse(currentData.GetProperty("dt_txt").GetString());
                if (date.Hour == 12)
                {
                    weatherInfos.Add(new WeatherForecast(
                        currentData.GetProperty("dt_txt").GetString(), //Date
                        currentData.GetProperty("main").GetProperty("temp").GetDouble(), //Temperature
                        currentData.GetProperty("weather")[0].GetProperty("description").GetString() ?? "", //Description
                        currentData.GetProperty("weather")[0].GetProperty("icon").GetString() ?? "", //Icon
                        currentData.GetProperty("weather")[0].GetProperty("icon").GetString() ?? "", //WeatherIcon
                        currentData.GetProperty("main").GetProperty("humidity").GetInt32(), //Humidity
                        currentData.GetProperty("wind").GetProperty("speed").GetDouble(), //WindSpeed
                        currentData.GetProperty("main").GetProperty("pressure").GetInt32(), //Pressure
                        currentData.GetProperty("clouds").GetProperty("all").GetInt32() //Clouds
                        ));
                }
            }

            return new List<object>() { weatherInfos, geoInfo };
        }

    }
}