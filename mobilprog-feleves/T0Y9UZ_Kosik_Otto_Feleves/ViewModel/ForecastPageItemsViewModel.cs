using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;

namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel
{
    public partial class ForecastPageItemsViewModel : ObservableObject
    {
        [ObservableProperty]
        private IWeatherForecast weatherForecastData;

        [ObservableProperty]
        private bool detailsVisible;

        [ObservableProperty]
        private string plusMinusPicture;

        [ObservableProperty]
        private Color cardColor;

        [ObservableProperty]
        private Color borderColor;

        [ObservableProperty]
        private string date;

        public ForecastPageItemsViewModel(IWeatherForecast weatherForecast, Color cardColor, Color borderColor)
        {
            this.WeatherForecastData = weatherForecast;
            this.CardColor = cardColor;
            this.BorderColor = borderColor;

            this.Date = DateOnly.Parse(this.WeatherForecastData.Date.Split(" ")[0]).DayOfWeek.ToString();
            this.DetailsVisible = false;
            this.PlusMinusPicture = "plusz.png";
        }

        [RelayCommand]
        private async Task ToggleDetails()
        {
            DetailsVisible = !DetailsVisible;
            PlusMinusPicture = DetailsVisible ? "minusz.png" : "plusz.png";
        }
    }
}
