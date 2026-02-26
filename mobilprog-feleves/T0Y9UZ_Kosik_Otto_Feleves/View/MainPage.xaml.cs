using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Shapes;
using System.Runtime.InteropServices;
using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves.View
{
    
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            BindingContext = vm;
            WeakReferenceMessenger.Default.Register<string>(this, async (r, m) =>
            {
                await DisplayAlert("Warning", m, "Ok");
            });
            InitializeComponent();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            WeakReferenceMessenger.Default.Unregister<string>(this);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var tmp = BindingContext as MainPageViewModel;

            await Task.Delay(1000);

            if (tmp.GeoInfo != null && tmp.WeatherForecasts != null)
            {
                TempImage.Source = $"https://openweathermap.org/img/wn/{tmp.WeatherForecasts[0].Icon}.png"; 
            }
        }
    }
}