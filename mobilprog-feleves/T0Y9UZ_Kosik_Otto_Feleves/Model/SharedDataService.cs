using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public class SharedDataService : ISharedDataService
    {
        public IGeoInfo? CurrentGeoInfo { get; set; }
        public List<IWeatherForecast>? CurrentWeatherForecasts { get; set; }
    }
}