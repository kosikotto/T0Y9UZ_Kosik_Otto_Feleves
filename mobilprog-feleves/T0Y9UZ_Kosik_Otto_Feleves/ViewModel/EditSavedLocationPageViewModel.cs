using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Text;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;

namespace T0Y9UZ_Kosik_Otto_Feleves.ViewModel
{

    [QueryProperty(nameof(SavedLocation), "savedLocation")]
    [QueryProperty(nameof(BackgroundColor), "BackgroundColor")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(MainPageViewModel), "MainPageViewModel")]

    public partial class EditSavedLocationPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private SavedLocation savedLocation;

        [ObservableProperty]
        private Color backgroundColor;

        [ObservableProperty]
        private Color textColor;

        [ObservableProperty]
        private string newSavedLocation;

        [ObservableProperty]
        private MainPageViewModel mainPageViewModel;

        public EditSavedLocationPageViewModel()
        {
            
        }

        [RelayCommand]
        private async void DoTheUpdate()
        {
            if(NewSavedLocation != null)
            {
                MainPageViewModel.Locations.Remove(SavedLocation);

                SavedLocation.Location = NewSavedLocation;
                NewSavedLocation = string.Empty;

                await MainPageViewModel.database.UpdateLocationAsync(SavedLocation);
                
                MainPageViewModel.Locations.Add(SavedLocation);

                await Shell.Current.GoToAsync("//MainPage");
            }
        }

        [RelayCommand]
        private async void CancelUpdate()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
