using Microsoft.Maui.Controls.Shapes;
using System.Threading.Tasks;
using T0Y9UZ_Kosik_Otto_Feleves.Model;
using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves.View;

public partial class ForecastPage : ContentPage
{
    ForecastPageViewModel vm;
    public ForecastPage(ForecastPageViewModel vm)
	{
        BindingContext = vm;
        this.vm = vm;
        InitializeComponent();
    }
}