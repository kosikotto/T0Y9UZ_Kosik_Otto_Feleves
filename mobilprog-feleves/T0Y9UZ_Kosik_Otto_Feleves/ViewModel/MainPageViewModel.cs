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
    [QueryProperty(nameof(GeoInfo), "geoInfo")]
    [QueryProperty(nameof(WeatherForecasts), "weatherForecasts")]
    [QueryProperty(nameof(BackgroundColor), "BackgroundColor")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(IsDarkTheme), "IsDarkTheme")] 
    public partial class MainPageViewModel : ObservableObject 
    { 
        private readonly NetworkAccess _current = Connectivity.Current.NetworkAccess;
        public ObservableCollection<SavedLocation> Locations { get; set; }
        [ObservableProperty]
        private SavedLocation selectedItem;

        private ILocationDatabase database;

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

            this.database = new LocationDatabase();
            Locations = new ObservableCollection<SavedLocation>();

            if(_current == NetworkAccess.Internet)
            {
                _ = InitializeWithCoordinates();
            }

            _ = InitializeAsync();
        }

        public async Task InitializeWithCoordinates()
        {
            var responseMessage = await WeatherService.FetchData();
            if (responseMessage != null)
            {
                await GenerateValues(responseMessage);
            }
            else
            {
                InitializeWithDefaultLocation();
            }
        }

        public async Task InitializeWithDefaultLocation()
        {
            string defaultLocation = Preferences.Default.Get("DefaultLocation", string.Empty);
            if (!string.IsNullOrEmpty(defaultLocation))
            {
                SearchInput = defaultLocation;
                _ = DisplayValues();
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

        public async Task InitializeAsync()
        {
            //await database.Clear();
            var locationList = await database.GetLocationsAsync();
            Locations.Clear();
            locationList.ForEach(x => Locations.Add(x));
        }

        [RelayCommand]
        private async Task AddLocationToFav()
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
        private async Task DeleteLocationFromFav()
        {
            if (SelectedItem != null)
            {
                await database.DeleteLocationAsync(SelectedItem);
                Locations.Remove(SelectedItem);
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