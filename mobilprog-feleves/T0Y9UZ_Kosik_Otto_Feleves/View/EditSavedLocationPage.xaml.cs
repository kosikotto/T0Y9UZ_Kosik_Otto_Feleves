using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves.View;

public partial class EditSavedLocationPage : ContentPage
{
	public EditSavedLocationPage()
	{
		InitializeComponent();
		BindingContext = new EditSavedLocationPageViewModel();
	}
}