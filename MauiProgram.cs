using ImiknWifiNavigationApp.IWNA.Database.Models;
using ImiknWifiNavigationApp.IWNA.Database.Services;
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

        // TODO move DataSource to a resource.resx or appsettings.json
        var connectionString = new SqliteConnectionStringBuilder { DataSource = @"D:\iwna.db" }.ConnectionString;
        
        builder.Services
            .AddDbContext<IwnaContext>(optionsBuilder => optionsBuilder.UseSqlite(connectionString));

        builder.Services
            .AddTransient<ILocationService, LocationService>()
            .AddTransient<INetworkService, NetworkService>()
            .AddTransient<MainPage>();
        
        return builder.Build();
    }
}