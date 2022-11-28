using ImiknWifiNavigationApp.IWNA.EF.Models;
using ImiknWifiNavigationApp.IWNA.EF.Services;
using ImiknWifiNavigationApp.IWNA.Misc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ImiknWifiNavigationApp;

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


#if ANDROID
        builder.Services
            .AddDbContext<IwnaContext>(optionsBuilder => optionsBuilder.UseSqlite(new SqliteConnectionStringBuilder()
            {
                DataSource = ResourceTransferUtility.ExtDatabasePath
            }.ConnectionString));
#else
        builder.Services
            .AddDbContext<IwnaContext>();
#endif

        builder.Services
            .AddSingleton<MainPage>()
            .AddTransient<IApService, ApService>()
            .AddTransient<ILocationService, LocationService>();

        return builder.Build();
    }
}