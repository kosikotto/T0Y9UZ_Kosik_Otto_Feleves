using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;

namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel
{
    [QueryProperty(nameof(GeoInfo), "geoInfo")]
    [QueryProperty(nameof(WeatherForecasts), "weatherForecasts")]
    [QueryProperty(nameof(IsForecastButtonEnabled), "IsForecastButtonEnabled")]
    [QueryProperty(nameof(BackgroundColor), "BackgroundColor")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(IsDarkTheme), "IsDarkTheme")]
    public partial class SettingsPageViewModel:ObservableObject
    {
        [ObservableProperty]
        private GeoInfo geoInfo;

        [ObservableProperty]
        private List<WeatherForecast> weatherForecasts;

        [ObservableProperty]
        private bool isForecastButtonEnabled;

        [ObservableProperty]
        private Color backgroundColor = Colors.DarkSlateBlue;

        [ObservableProperty]
        private Color textColor = Colors.White;

        [ObservableProperty]
        private bool isDarkTheme = true;

        public SettingsPageViewModel()
        {

        }

        [RelayCommand]
        public void ChangeTheme()
        {
            if(IsDarkTheme)
            {
                BackgroundColor = Colors.LightBlue;
                TextColor = Colors.Black;
                IsDarkTheme = false;
            }

            else
            {
                BackgroundColor = Colors.DarkSlateBlue;
                TextColor = Colors.White;
                IsDarkTheme = true;
            }
        }
    }
}