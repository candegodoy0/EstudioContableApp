using Microsoft.Extensions.Logging;
using EstudioContableApp.Data;
using EstudioContableApp.Services;
using EstudioContableApp.Repositories;
using EstudioContableApp.ViewModels;
using EstudioContableApp.Views;
using Plugin.LocalNotification;

namespace EstudioContableApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // servicios de datos
            builder.Services.AddSingleton<ClienteService>();
            builder.Services.AddSingleton<DatabaseService>();

            // repositorios
            builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();

            // viewmodels
            builder.Services.AddTransient<ClientesViewModel>();
            builder.Services.AddTransient<DetalleClienteViewModel>();

            // vistas
            builder.Services.AddTransient<ClientesPage>();
            builder.Services.AddTransient<DetalleClientePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
