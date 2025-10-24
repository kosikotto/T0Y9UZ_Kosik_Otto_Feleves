using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics.Text;
using T0Y9UZ_Kosik_Otto_Feleves.Model;
using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves.View;

[QueryProperty(nameof(GeoInfo), "geoInfo")]
[QueryProperty(nameof(WeatherForecasts), "weatherForecasts")]
public partial class ForecastPage : ContentPage
{
    public GeoInfo GeoInfo { get; set; }
    public List<WeatherForecast> WeatherForecasts { get; set; }
    public ForecastPage()
	{
		InitializeComponent();
        BindingContext = new ForecastPageViewModel();
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (GeoInfo != null && WeatherForecasts != null)
        {
            ForecastPageButton.IsEnabled = true;
            Generator();
        }
    }

    private async void Generator()
    {
        ForecastForFiveDays.Children.Clear();
        VerticalStackGenerate(WeatherForecasts[0], true, 0);

        for (int i = 1; i < WeatherForecasts.Count; i++)
        {
            VerticalStackGenerate(WeatherForecasts[i], false, i);
        }

        ForecastForFiveDaysContainer.IsVisible = true;
    }

    private async void VerticalStackGenerate(WeatherForecast weatherForecasts, bool current, int idx)
    {
        var itemVm = new ForecastPageItemsViewModel();
        var vm = BindingContext as ForecastPageViewModel;
        vm.ForecastItems.Add(itemVm);

        var color = current ? Brush.DarkGreen : Brush.DarkGray;

        // FONTOS: az aktuális "kártya" binding contextje
        var borderForStack = new Border
        {
            Stroke = Brush.White,
            StrokeThickness = 2,
            Background = color,
            StrokeShape = new RoundRectangle { CornerRadius = 10 },
            Margin = new Thickness(5),
            BindingContext = itemVm // <-- EZ KELL IDE!!!
        };

        var dayLabel = new Label
        {
            Text = $"{DateOnly.Parse(weatherForecasts.Date.Split(" ")[0]).DayOfWeek}",
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 30,
            TextColor = vm.TextColor
        };

        var weatherIcon = new Image
        {
            Source = $"https://openweathermap.org/img/wn/{weatherForecasts.Icon}.png",
            WidthRequest = 50,
            HeightRequest = 50
        };
        weatherIcon.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageItemsViewModel.DetailsVisible)));

        var weatherDescription = new Label
        {
            Text = $"{weatherForecasts.Description}",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor
        };
        weatherDescription.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageItemsViewModel.DetailsVisible)));

        var temp = new Label
        {
            Text = $"{weatherForecasts.Temperature}°C",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor
        };
        temp.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageItemsViewModel.DetailsVisible)));

        var humidity = new Label
        {
            Text = $"Humidity: {weatherForecasts.Humidity}%",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor
        };
        humidity.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageItemsViewModel.DetailsVisible)));

        var wind = new Label
        {
            Text = $"Wind: {weatherForecasts.WindSpeed} m/s",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor
        };
        wind.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageItemsViewModel.DetailsVisible)));

        var button = new Button
        {
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor,
            BackgroundColor = vm.BackgroundColor
        };
        button.SetBinding(Button.CommandProperty, new Binding(nameof(ForecastPageItemsViewModel.ToggleDetailsCommand)));
        button.SetBinding(Button.TextProperty, new Binding(nameof(ForecastPageItemsViewModel.TextOfDetailsButton)));

        var verticalStack = new VerticalStackLayout
        {
            Margin = 10,
            Spacing = 5,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 200,
        };

        verticalStack.Children.Add(dayLabel);
        verticalStack.Children.Add(weatherIcon);
        verticalStack.Children.Add(weatherDescription);
        verticalStack.Children.Add(temp);
        verticalStack.Children.Add(humidity);
        verticalStack.Children.Add(wind);
        verticalStack.Children.Add(button);

        borderForStack.Content = verticalStack;
        ForecastForFiveDays.Children.Add(borderForStack);
    }


    //Navigációs gombok
    private async void OnMainPageClick(object? sender, EventArgs e)
    {
        if ((BindingContext as ForecastPageViewModel).GeoInfo != null && (BindingContext as ForecastPageViewModel).WeatherForecasts != null)
        {
            //await Shell.Current.GoToAsync(($"//MainPage"), animate: true, parameters: new Dictionary<string, object>
            //{
            //    { "geoInfo", this.GeoInfo },
            //    { "weatherForecasts", this.WeatherForecasts }
            //});

            await Shell.Current.GoToAsync($"//MainPage", new ShellNavigationQueryParameters()
                {
                    { "geoInfo", (BindingContext as ForecastPageViewModel).GeoInfo },
                    { "weatherForecasts", (BindingContext as ForecastPageViewModel).WeatherForecasts },
                    { "BackgroundColor", (BindingContext as ForecastPageViewModel).BackgroundColor },
                    { "TextColor", (BindingContext as ForecastPageViewModel).TextColor },
                    { "IsDarkTheme", (BindingContext as ForecastPageViewModel).IsDarkTheme }
                });
        }
        else
        {
            await Shell.Current.GoToAsync($"//MainPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundColor", (BindingContext as ForecastPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as ForecastPageViewModel).TextColor },
                { "IsDarkTheme", (BindingContext as ForecastPageViewModel).IsDarkTheme }
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
                { "geoInfo", (BindingContext as ForecastPageViewModel).GeoInfo },
                { "weatherForecasts", (BindingContext as ForecastPageViewModel).WeatherForecasts },
                { "BackgroundColor", (BindingContext as ForecastPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as ForecastPageViewModel).TextColor },
                { "IsDarkTheme", (BindingContext as ForecastPageViewModel).IsDarkTheme }
            });
    }
    private async void OnSettingsPageClick(object? sender, EventArgs e)
    {
        if ((BindingContext as ForecastPageViewModel).GeoInfo != null && (BindingContext as ForecastPageViewModel).WeatherForecasts != null)
        {
            //await Shell.Current.GoToAsync(($"//SettingsPage"), animate: true, parameters: new Dictionary<string, object>
            //{
            //    { "geoInfo", this.GeoInfo },
            //    { "weatherForecasts", this.WeatherForecasts }
            //});

            await Shell.Current.GoToAsync($"//SettingsPage", animate: true, new ShellNavigationQueryParameters()
                {
                    { "geoInfo", (BindingContext as ForecastPageViewModel).GeoInfo },
                    { "weatherForecasts", (BindingContext as ForecastPageViewModel).WeatherForecasts },
                    { "BackgroundColor", (BindingContext as ForecastPageViewModel).BackgroundColor },
                    { "TextColor", (BindingContext as ForecastPageViewModel).TextColor },
                    { "IsForecastButtonEnabled", true },
                    { "IsDarkTheme", (BindingContext as ForecastPageViewModel).IsDarkTheme }
                });
        }
        else
        {
            await Shell.Current.GoToAsync($"//SettingsPage", animate: true, new ShellNavigationQueryParameters()
            {
                { "BackgroundColor", (BindingContext as ForecastPageViewModel).BackgroundColor },
                { "TextColor", (BindingContext as ForecastPageViewModel).TextColor },
                { "IsForecastButtonEnabled", true },
                { "IsDarkTheme", (BindingContext as ForecastPageViewModel).IsDarkTheme }
            });
        }
    }
}