using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;
namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel 
{ 
    [QueryProperty(nameof(GeoInfo), "geoInfo")]
    [QueryProperty(nameof(WeatherForecasts), "weatherForecasts")]
    [QueryProperty(nameof(BackgroundColor), "BackgroundColor")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(IsDarkTheme), "IsDarkTheme")] 
    public partial class MainPageViewModel : ObservableObject 
    { 
        private readonly NetworkAccess _current = Connectivity.Current.NetworkAccess;

        [ObservableProperty] 
        private string searchInput; 
        [ObservableProperty] 
        private GeoInfo geoInfo; 
        [ObservableProperty] 
        private List<WeatherForecast> weatherForecasts; 
        [ObservableProperty] 
        private bool isWeatherVisible; 
        [ObservableProperty] 
        private bool isDetailsVisible; 
        [ObservableProperty] 
        private bool isForecastButtonEnabled; 
        [ObservableProperty] 
        private Color backgroundColor = Colors.DarkSlateBlue; 
        [ObservableProperty] 
        private Color textColor = Colors.White; 
        [ObservableProperty] 
        private bool isDarkTheme = true; 
        public MainPageViewModel() 
        { 
            IsWeatherVisible = false; 
            IsDetailsVisible = false; 
            IsForecastButtonEnabled = false;

            if(_current == NetworkAccess.Internet)
            {
                string defaultLocation = Preferences.Default.Get("DefaultLocation", string.Empty);
                if (!string.IsNullOrEmpty(defaultLocation))
                {
                    SearchInput = defaultLocation;
                    _ = DisplayValues();
                }
            }
        }

        [RelayCommand] 
        private async Task DisplayValues() 
        { 
            if(_current == NetworkAccess.Internet)
            {
                string inputCity = SearchInput;
                if (!string.IsNullOrEmpty(inputCity))
                {
                    var responseMessage = await WeatherService.FetchData(SearchInput);
                    if (responseMessage != null)
                    {
                        SearchInput = "";
                        GenerateValues(responseMessage);
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send("Please check if you entered a valid input.");
                    }
                }
                else if (string.IsNullOrEmpty(inputCity))
                {
                    WeakReferenceMessenger.Default.Send("Please don't leave the input field empty.");
                }
            }

            else
            {
                WeakReferenceMessenger.Default.Send("No internet connection. Please check your network settings and restart the application.");
            }
        }
        private async Task GenerateValues(string responseMessage) 
        { 
            var tmp = WeatherService.ParseData(responseMessage); 
            GeoInfo = (await tmp)[1] as GeoInfo; 
            WeatherForecasts = (await tmp)[0] as List<WeatherForecast>; 
            IsForecastButtonEnabled = true; 
            IsWeatherVisible = true; IsDetailsVisible = true; 
        } 
    } 
}