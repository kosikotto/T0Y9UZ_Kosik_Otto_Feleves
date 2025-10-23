using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel
{
    public partial class ForecastPageItemsViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool detailsVisible;

        [ObservableProperty]
        private string textOfDetailsButton;

        public ForecastPageItemsViewModel()
        {
            this.DetailsVisible = false;
            this.TextOfDetailsButton = "+";
        }

        [RelayCommand]
        private void ToggleDetails()
        {
            DetailsVisible = !DetailsVisible;
            TextOfDetailsButton = DetailsVisible ? "-" : "+";
        }
    }
}
