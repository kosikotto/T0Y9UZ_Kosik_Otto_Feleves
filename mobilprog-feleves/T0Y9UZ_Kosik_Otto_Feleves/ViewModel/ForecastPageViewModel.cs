using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;
namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel { 

    [QueryProperty(nameof(GeoInfo), "geoInfo")]
    [QueryProperty(nameof(WeatherForecasts), "weatherForecasts")]
    [QueryProperty(nameof(BackgroundColor), "BackgroundColor")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(IsDarkTheme), "IsDarkTheme")] 
    public partial class ForecastPageViewModel : ObservableObject 
    { 
        [ObservableProperty] 
        private GeoInfo geoInfo; 

        [ObservableProperty] 
        private List<WeatherForecast> weatherForecasts; 

        [ObservableProperty] 
        private Color backgroundColor = Colors.DarkSlateBlue; 

        [ObservableProperty] 
        private Color textColor = Colors.White; 

        [ObservableProperty] 
        private bool isDarkTheme = true;

        [ObservableProperty]
        private ObservableCollection<ForecastPageItemsViewModel> forecastItems;


        public ForecastPageViewModel()
        {
            ForecastItems = new ObservableCollection<ForecastPageItemsViewModel>();
        }

        
    } 
}