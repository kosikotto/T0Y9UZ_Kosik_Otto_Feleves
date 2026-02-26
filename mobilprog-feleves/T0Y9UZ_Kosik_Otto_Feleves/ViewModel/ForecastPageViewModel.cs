 using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;
namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel { 

    [QueryProperty(nameof(BackgroundImage), "BackgroundImage")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(IsDarkTheme), "IsDarkTheme")]

    [QueryProperty(nameof(CardColor), "CardColor")]
    [QueryProperty(nameof(PlaceholderColor), "PlaceholderColor")]
    [QueryProperty(nameof(NavButtonsColor), "NavButtonsColor")]
    [QueryProperty(nameof(ButtonsColor), "ButtonsColor")]
    [QueryProperty(nameof(ForecastButtonColor), "ForecastButtonColor")]
    public partial class ForecastPageViewModel : ObservableObject
    {
        ISharedDataService sharedDataService;

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

        [ObservableProperty]
        private ObservableCollection<ForecastPageItemsViewModel> forecastItemsCollection;

        public ForecastPageViewModel(ISharedDataService sharedDataService)
        {
            this.sharedDataService = sharedDataService;

            ForecastItemsCollection = new ObservableCollection<ForecastPageItemsViewModel>();

            ForecastItemsCollection.Clear();

            var forecasts = sharedDataService?.CurrentWeatherForecasts;

            for (int i = 0; i < forecasts.Count; i++)
            {
                var forecastData = forecasts[i];

                var itemVm = new ForecastPageItemsViewModel(
                    forecastData,
                    i == 0 ? Colors.DarkGreen : CardColor,
                    i == 0 ? Color.FromArgb("#00FF00") : Color.FromArgb("#000999"));

                ForecastItemsCollection.Add(itemVm);
            }
        }

        //Navigációs gombok
        [RelayCommand]
        private async Task NavigateToMainAsync()
        {
            await Shell.Current.GoToAsync($"//MainPage", new ShellNavigationQueryParameters()
                {
                    { "BackgroundImage", BackgroundImage },
                    { "TextColor", TextColor },
                    { "IsDarkTheme", IsDarkTheme },
                    { "CardColor", CardColor },
                    { "PlaceholderColor", PlaceholderColor },
                    { "NavButtonsColor", NavButtonsColor },
                    { "ForecastButtonColor", ForecastButtonColor },
                    { "ButtonsColor", ButtonsColor },
                });
        }

        [RelayCommand]
        private async Task NavigateToSettingsAsync()
        {
            await Shell.Current.GoToAsync($"//SettingsPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundImage", BackgroundImage },
                { "TextColor", TextColor },
                { "IsForecastButtonEnabled", true },
                { "IsDarkTheme", IsDarkTheme },
                { "CardColor", CardColor },
                { "PlaceholderColor", PlaceholderColor },
                { "NavButtonsColor", NavButtonsColor },
                { "ButtonsColor", ButtonsColor },
                { "ForecastButtonColor", ForecastButtonColor }
            });
        }
    } 
}