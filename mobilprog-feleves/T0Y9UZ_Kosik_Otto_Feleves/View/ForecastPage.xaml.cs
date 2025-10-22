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

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (GeoInfo != null && WeatherForecasts != null)
        {
            ForecastPageButton.IsEnabled = true;
            Generator();
        }
    }

    public void Generator()
    {
        ForecastForFiveDays.Children.Clear();
        VerticalStackGenerate(WeatherForecasts[0], true);

        for (int i = 1; i < WeatherForecasts.Count; i++)
        {
            VerticalStackGenerate(WeatherForecasts[i], false);
        }

        ForecastForFiveDaysContainer.IsVisible = true;
    }

    public void VerticalStackGenerate(WeatherForecast weatherForecasts, bool current)
    {
        var color = Brush.DarkGray;
        var vm = BindingContext as ForecastPageViewModel;

        if (current)
        {
            color = Brush.DarkGreen;
        }

        var dayLable = new Label
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
            HeightRequest = 50,
        };

        weatherIcon.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageViewModel.DetailsVisible)));

        var weatherDescription = new Label
        {
            Text = $"{weatherForecasts.Description}",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor
        };

        weatherDescription.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageViewModel.DetailsVisible)));

        var temp = new Label
        {
            Text = $"{weatherForecasts.Temperature}°C",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor
        };

        temp.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageViewModel.DetailsVisible)));

        var humidity = new Label
        {
            Text = $"Humidity: {weatherForecasts.Humidity}%",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor
        };

        humidity.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageViewModel.DetailsVisible)));

        var wind = new Label
        {
            Text = $"Wind: {weatherForecasts.WindSpeed} m/s",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = vm.TextColor
        };

        wind.SetBinding(IsVisibleProperty, new Binding(nameof(ForecastPageViewModel.DetailsVisible)));

        var borderForStack = new Border
        {
            Stroke = Brush.White,
            StrokeThickness = 2,
            Background = color,

            StrokeShape = new RoundRectangle
            {
                CornerRadius = 10
            }
        };

        var button = new Button
        {
            HorizontalOptions = LayoutOptions.Center,
            Command = vm.ToggleDetailsCommand,
            TextColor = vm.TextColor,
            BackgroundColor = vm.BackgroundColor
        };

        button.SetBinding(Button.TextProperty, new Binding(nameof(ForecastPageViewModel.TextOfDetailsButton)));

        var verticalStack = new VerticalStackLayout
        {
            Margin = 5,
            Spacing = 5,
            HorizontalOptions = LayoutOptions.Center
        };

        verticalStack.Children.Add(dayLable);
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