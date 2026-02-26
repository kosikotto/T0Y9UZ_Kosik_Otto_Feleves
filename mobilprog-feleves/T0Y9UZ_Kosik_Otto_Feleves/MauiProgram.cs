using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration.Json;
using T0Y9UZ_Kosik_Otto_Feleves.ViewModel;
using T0Y9UZ_Kosik_Otto_Feleves.View;
using T0Y9UZ_Kosik_Otto_Feleves.Model;

namespace T0Y9UZ_Kosik_Otto_Feleves
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ISharedDataService, SharedDataService>();

            builder.Services.AddSingleton<IWeatherService, WeatherService>();
            builder.Services.AddSingleton<ILocationDatabase, LocationDatabase>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<ForecastPage>();
            builder.Services.AddTransient<ForecastPageViewModel>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsPageViewModel>();
            builder.Services.AddSingleton<EditSavedLocationPage>();
            builder.Services.AddSingleton<EditSavedLocationPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}