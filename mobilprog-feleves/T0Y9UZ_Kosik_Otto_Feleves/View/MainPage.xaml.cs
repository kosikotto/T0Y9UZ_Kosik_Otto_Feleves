using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Shapes;
using System.Runtime.InteropServices;
using System.Text.Json;
using T0Y9UZ_Kosik_Otto_Feleves.Model;
using T0Y9UZ_Kosik_Otto_Feleves.View;
using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves.View
{
    
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();

            WeakReferenceMessenger.Default.Register<string>(this, async (r, m) =>
            {
                await DisplayAlert("Warning", m, "Ok");
            });
        }

        //Navigációs gombok
        private async void OnMainPageClick(object? sender, EventArgs e)
        {
            if ((BindingContext as MainPageViewModel).GeoInfo != null && (BindingContext as MainPageViewModel).WeatherForecasts != null)
            {
                //await Shell.Current.GoToAsync(($"//MainPage"), animate: true, parameters: new Dictionary<string, object>
                //{
                //    { "geoInfo", this.GeoInfo },
                //    { "weatherForecasts", this.WeatherForecasts }
                //});

                await Shell.Current.GoToAsync($"//MainPage", new ShellNavigationQueryParameters()
                {
                    { "geoInfo", (BindingContext as MainPageViewModel).GeoInfo },
                    { "weatherForecasts", (BindingContext as MainPageViewModel).WeatherForecasts },
                    { "BackgroundColor", (BindingContext as MainPageViewModel).BackgroundColor },
                    { "TextColor", (BindingContext as MainPageViewModel).TextColor },
                    { "IsDarkTheme", (BindingContext as MainPageViewModel).IsDarkTheme }
                });
            }
            else
            {
                await Shell.Current.GoToAsync($"//MainPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundColor", (BindingContext as MainPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as MainPageViewModel).TextColor },
                { "IsDarkTheme", (BindingContext as MainPageViewModel).IsDarkTheme }
            });
            }
        }
        private async void OnForecastPageClick(object? sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync(($"//ForecastPage"), animate: true, parameters: new Dictionary<string, object>
            //{
            //    { "geoInfo", this.GeoInfo },
            //    { "weatherForecasts", this.WeatherForecasts }
            //});

            await Shell.Current.GoToAsync($"//ForecastPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "geoInfo", (BindingContext as MainPageViewModel).GeoInfo },
                { "weatherForecasts", (BindingContext as MainPageViewModel).WeatherForecasts },
                { "BackgroundColor", (BindingContext as MainPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as MainPageViewModel).TextColor },
                { "IsDarkTheme", (BindingContext as MainPageViewModel).IsDarkTheme }
            });
        }
        private async void OnSettingsPageClick(object? sender, EventArgs e)
        {
            if ((BindingContext as MainPageViewModel).GeoInfo != null && (BindingContext as MainPageViewModel).WeatherForecasts != null)
            {
                //await Shell.Current.GoToAsync(($"//SettingsPage"), animate: true, parameters: new Dictionary<string, object>
                //{
                //    { "geoInfo", this.GeoInfo },
                //    { "weatherForecasts", this.WeatherForecasts }
                //});

                await Shell.Current.GoToAsync($"//SettingsPage", animate: true, new ShellNavigationQueryParameters()
                {
                    { "geoInfo", (BindingContext as MainPageViewModel).GeoInfo },
                    { "weatherForecasts", (BindingContext as MainPageViewModel).WeatherForecasts },
                    { "BackgroundColor", (BindingContext as MainPageViewModel).BackgroundColor },
                    { "IsForecastButtonEnabled", (BindingContext as MainPageViewModel).IsForecastButtonEnabled },
                    { "TextColor", (BindingContext as MainPageViewModel).TextColor },
                    { "IsDarkTheme", (BindingContext as MainPageViewModel).IsDarkTheme }
                });
            }
            else
            {
                await Shell.Current.GoToAsync($"//SettingsPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundColor", (BindingContext as MainPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as MainPageViewModel).TextColor },
                { "IsForecastButtonEnabled", (BindingContext as MainPageViewModel).IsForecastButtonEnabled },
                { "IsDarkTheme", (BindingContext as MainPageViewModel).IsDarkTheme }
            });
            }
        }
    }
}