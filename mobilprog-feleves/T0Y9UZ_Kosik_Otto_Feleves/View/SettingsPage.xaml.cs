using T0Y9UZ_Kosik_Otto_Feleves.Model;
using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves.View;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
	{
		InitializeComponent();
        BindingContext = new SettingsPageViewModel();
    }

    private async void OnMainPageClick(object? sender, EventArgs e)
    {
        if ((BindingContext as SettingsPageViewModel).GeoInfo != null && (BindingContext as SettingsPageViewModel).WeatherForecasts != null)
        {
            //await Shell.Current.GoToAsync(($"//MainPage"), animate: true, parameters: new Dictionary<string, object>
            //{
            //    { "geoInfo", this.GeoInfo },
            //    { "weatherForecasts", this.WeatherForecasts }
            //});

            await Shell.Current.GoToAsync($"//MainPage", new ShellNavigationQueryParameters()
                {
                    { "geoInfo", (BindingContext as SettingsPageViewModel).GeoInfo },
                    { "weatherForecasts", (BindingContext as SettingsPageViewModel).WeatherForecasts },
                    { "BackgroundColor", (BindingContext as SettingsPageViewModel).BackgroundColor },
                    { "TextColor", (BindingContext as SettingsPageViewModel).TextColor },
                    { "IsDarkTheme", (BindingContext as SettingsPageViewModel).IsDarkTheme }
                });
        }
        else
        {
            await Shell.Current.GoToAsync($"//MainPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundColor", (BindingContext as SettingsPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as SettingsPageViewModel).TextColor },
                { "IsDarkTheme", (BindingContext as SettingsPageViewModel).IsDarkTheme }
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
                { "geoInfo", (BindingContext as SettingsPageViewModel).GeoInfo },
                { "weatherForecasts", (BindingContext as SettingsPageViewModel).WeatherForecasts },
                { "BackgroundColor", (BindingContext as SettingsPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as SettingsPageViewModel).TextColor },
                { "IsDarkTheme", (BindingContext as SettingsPageViewModel).IsDarkTheme }
            });
    }
    private async void OnSettingsPageClick(object? sender, EventArgs e)
    {
        if ((BindingContext as SettingsPageViewModel).GeoInfo != null && (BindingContext as SettingsPageViewModel).WeatherForecasts != null)
        {
            //await Shell.Current.GoToAsync(($"//SettingsPage"), animate: true, parameters: new Dictionary<string, object>
            //{
            //    { "geoInfo", this.GeoInfo },
            //    { "weatherForecasts", this.WeatherForecasts }
            //});

            await Shell.Current.GoToAsync($"//SettingsPage", animate: true, new ShellNavigationQueryParameters()
                {
                    { "geoInfo", (BindingContext as SettingsPageViewModel).GeoInfo },
                    { "weatherForecasts", (BindingContext as SettingsPageViewModel).WeatherForecasts },
                    { "BackgroundColor", (BindingContext as SettingsPageViewModel).BackgroundColor },
                    { "TextColor", (BindingContext as SettingsPageViewModel).TextColor },
                    { "IsForecastButtonEnabled", (BindingContext as MainPageViewModel).IsForecastButtonEnabled },
                    { "IsDarkTheme", (BindingContext as SettingsPageViewModel).IsDarkTheme }
                });
        }
        else
        {
            await Shell.Current.GoToAsync($"//SettingsPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundColor", (BindingContext as SettingsPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as SettingsPageViewModel).TextColor },
                { "IsForecastButtonEnabled", (BindingContext as MainPageViewModel).IsForecastButtonEnabled },
                { "IsDarkTheme", (BindingContext as SettingsPageViewModel).IsDarkTheme }
            });
        }
    }
}