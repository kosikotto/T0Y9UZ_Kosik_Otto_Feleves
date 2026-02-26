using CommunityToolkit.Mvvm.Messaging;
using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves.View;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsPageViewModel vm)
	{
        BindingContext = vm;
        WeakReferenceMessenger.Default.Register<string>(this, async (r, m) =>
        {
            await DisplayAlert("Warning", m, "Ok");
        });
        InitializeComponent();
    }
}