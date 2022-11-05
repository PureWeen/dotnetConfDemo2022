using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dotnetConfDemo;

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

        builder
            .Services
            .AddSingleton(() =>
            {
                return (IApplication)Application.Current;
            })
            .AddScoped<Window>((sp) =>
            {
                var windows = sp.GetService<IApplication>().Windows;
                return (Window)windows.Last();
            })
            .AddScoped(sp =>
            {
                return (Shell)sp.GetService<Window>().Page;
            })
            .AddScoped(sp =>
            {
                return sp.GetService<Shell>().Navigation;
            })
            .AddScoped<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
