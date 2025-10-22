using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;

namespace T0Y9UZ_Kosik_Otto_Feleves
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}