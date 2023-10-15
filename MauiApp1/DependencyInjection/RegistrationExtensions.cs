using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Services.ImageResize;
using MauiApp1.Services.ImageResize.Platform;

namespace MauiApp1.DependencyInjection
{
    public static class RegistrationExtensions
    {
        public static void RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IImageResize, ImageResizeService>();

            //mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
            // Omitted for brevity...
        }

        public static void RegisterAppViewModel(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainPageVM>();

            //mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
            // Omitted for brevity...
        }

        public static void RegisterAppPages(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainPage>();

            //mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
            // Omitted for brevity...
        }
    }
}
