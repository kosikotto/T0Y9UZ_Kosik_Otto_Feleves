using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;
namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel 
{ 
    [QueryProperty(nameof(BackgroundImage), "BackgroundImage")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(IsDarkTheme), "IsDarkTheme")]
    [QueryProperty(nameof(CardColor), "CardColor")]
    [QueryProperty(nameof(PlaceholderColor), "PlaceholderColor")]
    [QueryProperty(nameof(NavButtonsColor), "NavButtonsColor")]
    [QueryProperty(nameof(ForecastButtonColor), "ForecastButtonColor")]
    [QueryProperty(nameof(ButtonsColor), "ButtonsColor")]


    [QueryProperty(nameof(UpdateSelectedItem), "UpdateSelectedItem")]
    public partial class MainPageViewModel : ObservableObject 
    {
        ISharedDataService sharedDataService;
        IWeatherService weatherService;
        private readonly NetworkAccess _current = Connectivity.Current.NetworkAccess;
        public ObservableCollection<SavedLocation> Locations { get; set; }
        [ObservableProperty]
        private SavedLocation selectedItem;

        [ObservableProperty]
        private SavedLocation updateSelectedItem;

        public ILocationDatabase database { get; set; }

        [ObservableProperty] 
        private string searchInput; 
        [ObservableProperty] 
        private IGeoInfo geoInfo;
        [ObservableProperty]
        private List<IWeatherForecast> weatherForecasts;
        [ObservableProperty] 
        private bool isWeatherVisible; 
        [ObservableProperty] 
        private bool isDetailsVisible; 
        [ObservableProperty] 
        private bool isForecastButtonEnabled; 
        [ObservableProperty] 
        private string backgroundImage; 
        [ObservableProperty] 
        private Color textColor; 
        [ObservableProperty] 
        private bool isDarkTheme;
        [ObservableProperty]
        private Color cardColor;
        [ObservableProperty]
        private Color placeholderColor;
        [ObservableProperty]
        private Color navButtonsColor;
        [ObservableProperty]
        private Color buttonsColor;
        [ObservableProperty]
        private Color forecastButtonColor;

        public MainPageViewModel(IWeatherService weatherService, ILocationDatabase database, ISharedDataService sharedDataService) 
        {
            this.weatherService = weatherService;
            this.database = database;
            this.sharedDataService = sharedDataService;
            this.GeoInfo = sharedDataService.CurrentGeoInfo;
            this.WeatherForecasts = sharedDataService.CurrentWeatherForecasts;

            //Preferences.Clear();
            BackgroundImage = Preferences.Default.Get("Background", "wallpaper5.jpg");
            TextColor = Color.FromArgb(Preferences.Default.Get("TextColor", "#FFFFFF"));
            IsDarkTheme = Preferences.Default.Get("IsDarkTheme", true);
            CardColor = Color.FromArgb(Preferences.Default.Get("CardColor", "#000059"));
            PlaceholderColor = Color.FromArgb(Preferences.Default.Get("PlaceholderColor", "#004999"));
            NavButtonsColor = Color.FromArgb(Preferences.Default.Get("NavButtonsColor", "#005999"));
            ButtonsColor = Color.FromArgb(Preferences.Default.Get("ButtonsColor", "#000599"));
            ForecastButtonColor = Color.FromArgb("#000039");

            IsWeatherVisible = false; 
            IsDetailsVisible = false; 
            IsForecastButtonEnabled = false;

            Locations = new ObservableCollection<SavedLocation>();

            if (_current == NetworkAccess.Internet)
            {
                _ = InitializeWithCoordinates();
            }

            _ = InitializeAsync();
        }

        private async Task InitializeWithCoordinates()
        {
            var responseMessage = await this.weatherService.FetchDataAsnyc();
            if (responseMessage != null)
            {
                GenerateValues(responseMessage);
            }
            else
            {
                InitializeWithDefaultLocation();
            }
        }

        private void InitializeWithDefaultLocation()
        {
            string defaultLocation = Preferences.Default.Get("DefaultLocation", string.Empty);
            if (!string.IsNullOrEmpty(defaultLocation))
            {
                SearchInput = defaultLocation;
                _ = DisplayValuesAsync();
            }
        }

        [RelayCommand] 
        private async Task DisplayValuesAsync() 
        { 
            if(_current == NetworkAccess.Internet)
            {
                string inputCity = SearchInput;
                if (!string.IsNullOrEmpty(inputCity))
                {
                    var responseMessage = await this.weatherService.FetchDataAsnyc(SearchInput);
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

        public async Task InitializeAsync()
        {
            //await database.Clear();
            var locationList = await database.GetLocationsAsync();
            Locations.Clear();
            locationList.ForEach(x => Locations.Add(x));
        }

        [RelayCommand]
        private async Task AddLocationToFavAsync()
        {
            if(GeoInfo != null)
            {
                SavedLocation location = new SavedLocation($"{GeoInfo.Country} - {GeoInfo.CityName}");
                var tmp = await database.GetLocationAsync(location);
                if (tmp != null)
                {
                    WeakReferenceMessenger.Default.Send("This location is already in your favorites.");
                    return;
                }

                else
                {
                    await database.CreateLocationAsync(location);
                    Locations.Add(location);
                }
            }
        }

        [RelayCommand]
        private async Task DeleteLocationFromFavAsync()
        {
            if (SelectedItem != null)
            {
                await database.DeleteLocationAsync(SelectedItem);
                Locations.Remove(SelectedItem);
            }
        }

        [RelayCommand]
        private async Task UpdateLocationFromFavAsync()
        {
            if(SelectedItem != null)
            {
                await Shell.Current.GoToAsync("//EditSavedLocationPage", new ShellNavigationQueryParameters()
                {
                    { "savedLocation", SelectedItem },
                    { "BackgroundImage", BackgroundImage },
                    { "TextColor", TextColor },
                    { "CardColor", CardColor },
                    { "PlaceholderColor", PlaceholderColor },
                    { "MainPageViewModel", this }
                });
            }
        }

        private void GenerateValues(string responseMessage) 
        { 
            var tmp = this.weatherService.ParseData(responseMessage); 
            var geoInfoValue = tmp[1] as IGeoInfo;
            if (geoInfoValue is not null)
            {
                GeoInfo = geoInfoValue;

                var weatherForecastsValue = tmp[0] as List<IWeatherForecast>;
                if (weatherForecastsValue is not null)
                {
                    WeatherForecasts = weatherForecastsValue;
                }
                else
                {
                    WeatherForecasts = new List<IWeatherForecast>();
                }

                this.sharedDataService.CurrentGeoInfo = GeoInfo;
                this.sharedDataService.CurrentWeatherForecasts = WeatherForecasts;

                IsForecastButtonEnabled = true;
                IsWeatherVisible = true;
                IsDetailsVisible = true;
                ForecastButtonColor = NavButtonsColor;
            }
        }

        [RelayCommand]
        private async Task NavigateToForecastAsync()
        {
            await Shell.Current.GoToAsync($"//ForecastPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundImage", BackgroundImage },
                { "TextColor", TextColor },
                { "IsDarkTheme", IsDarkTheme },
                { "CardColor", CardColor },
                { "PlaceholderColor", PlaceholderColor },
                { "NavButtonsColor", NavButtonsColor },
                { "ForecastButtonColor", ForecastButtonColor },
                { "ButtonsColor", ButtonsColor }
            });
        }

        [RelayCommand]
        private async Task NavigateToSettingsAsync()
        {
            await Shell.Current.GoToAsync($"//SettingsPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundImage", BackgroundImage },
                { "IsForecastButtonEnabled", IsForecastButtonEnabled },
                { "CardColor", CardColor },
                { "TextColor", TextColor },
                { "IsDarkTheme", IsDarkTheme },
                { "PlaceholderColor", PlaceholderColor },
                { "NavButtonsColor", NavButtonsColor },
                { "ForecastButtonColor", ForecastButtonColor },
                { "ButtonsColor", ButtonsColor },
            });
        }
    } 
}