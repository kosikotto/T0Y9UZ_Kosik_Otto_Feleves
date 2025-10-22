using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace T0Y9UZ_Kosik_Otto_Feleves
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
