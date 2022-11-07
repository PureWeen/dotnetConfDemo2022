using dotnetConfDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Foldable;

namespace dotnetConfDemo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseFoldable()
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
            .AddSingleton<AppShell>()
            .AddSingleton<RootWindow>()
            .AddSingleton<ChatConversationService>()
            .AddScoped<Window>((sp) =>
            {
                var application = sp.GetService<IApplication>();

                if (application.Windows.Count == 0)
                    return sp.GetService<RootWindow>();

                return (Window)application.Windows.Last();
            })
            .AddScoped<Shell>(sp =>
            {
                return (Shell)sp.GetService<Window>().Page ?? throw new Exception("Shell not initialized for this Window");
            })
            .AddScoped(sp =>
            {
                return sp.GetService<Shell>().Navigation;
            })
            .AddScoped<ChatWindow>()
            .AddScoped<ChatConversationShell>()
            .AddTransient<Calendar>()
            .AddTransient<Chat>()
            .AddTransient<PushedChatConversation>()
            .AddTransient<ContentPage>(); // This is just here for testing purposes

        Routing.RegisterRoute("PushedChatConversation", typeof(PushedChatConversation));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
