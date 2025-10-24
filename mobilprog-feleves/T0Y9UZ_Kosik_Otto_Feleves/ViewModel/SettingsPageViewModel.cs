using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
    public partial class SettingsPageViewModel : ObservableObject 
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
        [ObservableProperty] 
        private string defaultLocationProperty;
        [ObservableProperty]
        private string defaultLocationLable;
        public string DefaultLocation 
        { 
            get 
            { 
                return Preferences.Default.Get("DefaultLocation", string.Empty); 
            } 
            set 
            { 
                if (!string.IsNullOrEmpty(value)) 
                { 
                    Preferences.Default.Set("DefaultLocation", value); 
                    OnPropertyChanged(); 
                } 
            } 
        }

        [ObservableProperty]
        private bool resetButtonEnabled = false;

        public SettingsPageViewModel()
        {
            DefaultLocationLable = Preferences.Default.Get("DefaultLocation", "Budapest");
            ResetButtonEnabled = DefaultLocation != string.Empty;
        }

        [RelayCommand] 
        private void ChangeTheme() 
        { 
            if (IsDarkTheme) 
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
        [RelayCommand] 
        private async void ChangeDefaultLocation() 
        { 
            if (!string.IsNullOrEmpty(DefaultLocationProperty)) 
            { 
                var tmp = await WeatherService.FetchData(DefaultLocationProperty);
                if (tmp != null)
                {
                    Preferences.Default.Set("DefaultLocation", DefaultLocationProperty);
                    DefaultLocationLable = DefaultLocationProperty;
                    DefaultLocationProperty = string.Empty;
                    ResetButtonEnabled = true;
                    WeakReferenceMessenger.Default.Send("Default location has been updated successfully, please restart the application to apply the changes.");
                }

                else
                {
                    WeakReferenceMessenger.Default.Send("Please check if you have entered a valid location.");
                }
            } 
            else 
            { 
                WeakReferenceMessenger.Default.Send("Default location cannot be empty!"); 
            }
        }

        [RelayCommand]
        private async void ResetDefaultLocation()
        {
            Preferences.Default.Remove("DefaultLocation");
            DefaultLocationLable = "Budapest";
            ResetButtonEnabled = false;
            WeakReferenceMessenger.Default.Send("Default location has been reseted successfully, please restart the application to apply changes.");
        }
    } 
}