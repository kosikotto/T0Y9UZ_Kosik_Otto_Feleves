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
    [QueryProperty(nameof(BackgroundImage), "BackgroundImage")]
    [QueryProperty(nameof(TextColor), "TextColor")]
    [QueryProperty(nameof(MainPageViewModel), "MainPageViewModel")]

    [QueryProperty(nameof(CardColor), "CardColor")]
    [QueryProperty(nameof(PlaceholderColor), "PlaceholderColor")]
    [QueryProperty(nameof(NavButtonsColor), "NavButtonsColor")]
    [QueryProperty(nameof(ButtonsColor), "ButtonsColor")]
    public partial class EditSavedLocationPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private SavedLocation savedLocation;

        [ObservableProperty]
        private string backgroundImage;

        [ObservableProperty]
        private Color textColor;

        [ObservableProperty]
        private Color cardColor;

        [ObservableProperty]
        private Color placeholderColor;

        [ObservableProperty]
        private Color navButtonsColor;

        [ObservableProperty]
        private Color buttonsColor;

        [ObservableProperty]
        private string newSavedLocation;

        [ObservableProperty]
        private MainPageViewModel mainPageViewModel;

        public EditSavedLocationPageViewModel()
        {
            
        }

        [RelayCommand]
        private async Task DoTheUpdateAsync()
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
        private async Task CancelUpdateAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
