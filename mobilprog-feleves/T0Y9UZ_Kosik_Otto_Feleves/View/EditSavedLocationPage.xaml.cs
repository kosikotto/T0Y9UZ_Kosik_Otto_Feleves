using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves.View;

public partial class EditSavedLocationPage : ContentPage
{
	public EditSavedLocationPage(EditSavedLocationPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}