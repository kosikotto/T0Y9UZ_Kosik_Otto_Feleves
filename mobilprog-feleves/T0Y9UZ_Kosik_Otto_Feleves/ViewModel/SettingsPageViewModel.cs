using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;
namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel 
{ 
    [QueryProperty(nameof(IsForecastButtonEnabled), "IsForecastButtonEnabled")]
    [QueryProperty(nameof(Background), "BackgroundImage")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(CardColor), "CardColor")]
    [QueryProperty(nameof(PlaceholderColor), "PlaceholderColor")]
    [QueryProperty(nameof(NavButtonsColor), "NavButtonsColor")]
    [QueryProperty(nameof(ButtonsColor), "ButtonsColor")]
    [QueryProperty(nameof(ForecastButtonColor), "ForecastButtonColor")]
    public partial class SettingsPageViewModel : ObservableObject 
    {
        IWeatherService weatherService { get; set; }
        ISharedDataService sharedDataService;

        [ObservableProperty] 
        private IGeoInfo geoInfo; 
        [ObservableProperty] 
        private List<IWeatherForecast> weatherForecasts; 
        [ObservableProperty] 
        private bool isForecastButtonEnabled; 
        [ObservableProperty] 
        private string background; 
        [ObservableProperty] 
        private Color textColor; 
        [ObservableProperty] 
        private bool isDarkTheme;
        [ObservableProperty]
        private Color cardColor;
        [ObservableProperty] 
        private string defaultLocationProperty;
        [ObservableProperty]
        private string defaultLocationLable;
        [ObservableProperty]
        private Color resetColor;
        [ObservableProperty]
        private Color placeholderColor;
        [ObservableProperty]
        private Color navButtonsColor;
        [ObservableProperty]
        private Color buttonsColor;
        [ObservableProperty]
        private Color forecastButtonColor;
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

        public SettingsPageViewModel(IWeatherService weatherService, ISharedDataService sharedDataService)
        {
            this.weatherService = weatherService;
            this.sharedDataService = sharedDataService;

            this.GeoInfo = this.sharedDataService.CurrentGeoInfo;
            this.WeatherForecasts = this.sharedDataService.CurrentWeatherForecasts;

            DefaultLocationLable = Preferences.Default.Get("DefaultLocation", "Budapest");
            ResetButtonEnabled = DefaultLocation != string.Empty;
            ResetColor = DefaultLocation != string.Empty ? Colors.DarkRed : Color.FromArgb("#000039");
            IsDarkTheme = Preferences.Default.Get("IsDarkTheme", true);
        }

        public void SetSettings()
        {
            Preferences.Default.Set("CardColor", CardColor.ToArgbHex());
            Preferences.Default.Set("TextColor", TextColor.ToArgbHex());
            Preferences.Default.Set("PlaceholderColor", PlaceholderColor.ToArgbHex());
            Preferences.Default.Set("NavButtonsColor", NavButtonsColor.ToArgbHex());
            Preferences.Default.Set("ButtonsColor", ButtonsColor.ToArgbHex());
            Preferences.Default.Set("Background", Background);
            Preferences.Default.Set("IsDarkTheme", IsDarkTheme);
            OnPropertyChanged();
        }

        [RelayCommand] 
        private void ChangeTheme() 
        {
            if (!IsDarkTheme) 
            {
                Background = "wallpaper5.jpg";
                CardColor = Color.FromArgb("#000059");
                PlaceholderColor = Color.FromArgb("004999");
                NavButtonsColor = Color.FromArgb("#005999");
                ButtonsColor = Color.FromArgb("#000599");
                IsDarkTheme = true;

                if (IsForecastButtonEnabled)
                {
                    ForecastButtonColor = NavButtonsColor;
                }
            } 
            else if (IsDarkTheme)
            {
                Background = "wallpaper6.jpg";
                CardColor = Color.FromArgb("#212969");
                PlaceholderColor = Color.FromArgb("007999");
                NavButtonsColor = Color.FromArgb("#003999");
                ButtonsColor = Color.FromArgb("#000060");
                IsDarkTheme = false;

                if(IsForecastButtonEnabled)
                {
                    ForecastButtonColor = NavButtonsColor;
                }
            }

            SetSettings();
        } 

        [RelayCommand] 
        private async Task ChangeDefaultLocationAsync() 
        { 
            if (!string.IsNullOrEmpty(DefaultLocationProperty)) 
            { 
                var tmp = await weatherService.FetchDataAsnyc(DefaultLocationProperty);
                if (tmp != null)
                {
                    Preferences.Default.Set("DefaultLocation", DefaultLocationProperty);
                    DefaultLocationLable = DefaultLocationProperty;
                    DefaultLocationProperty = string.Empty;
                    ResetButtonEnabled = true;
                    ResetColor = Colors.DarkRed;
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
        private void ResetDefaultLocation()
        {
            Preferences.Default.Remove("DefaultLocation");
            DefaultLocationLable = "Budapest";
            ResetButtonEnabled = false;
            ResetColor = Color.FromArgb("#000039");
            WeakReferenceMessenger.Default.Send("Default location has been reseted successfully, please restart the application to apply changes.");
        }

        [RelayCommand]
        private async Task NavigateToMainAsync()
        {
            await Shell.Current.GoToAsync($"//MainPage", new ShellNavigationQueryParameters()
            {
                { "BackgroundImage", Background },
                { "CardColor", CardColor },
                { "TextColor", TextColor },
                { "IsDarkTheme", IsDarkTheme },
                { "PlaceholderColor", PlaceholderColor },
                { "NavButtonsColor", NavButtonsColor },
                { "ForecastButtonColor", ForecastButtonColor },
                { "ButtonsColor", ButtonsColor }
            });
        }

        [RelayCommand]
        private async Task NavigateToForecastAsync()
        {
            await Shell.Current.GoToAsync($"//ForecastPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundImage", Background },
                { "CardColor", CardColor },
                { "TextColor", TextColor },
                { "PlaceholderColor", PlaceholderColor },
                { "NavButtonsColor", NavButtonsColor },
                { "ForecastButtonColor", ForecastButtonColor },
                { "ButtonsColor", ButtonsColor }
            });
        }
    } 
}