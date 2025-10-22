using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public class GeoInfo
    {
        public string Country { get; private set; }
        public string CityName { get; private set; }
        public DateTime CurrentDate { get; private set; }

        public GeoInfo(string country, string cityName, DateTime currentDate)
        {
            Country = country;
            CityName = cityName;
            CurrentDate = currentDate;
        }
    }
}
