using ImiknWifiNavigationApp.IWNA.Database.Services;
using ImiknWifiNavigationApp.IWNA.Misc;

namespace ImiknWifiNavigationApp;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _provider;

    public MainPage(IServiceProvider provider)
    {
        _provider = provider;
        InitializeComponent();
        LoadFloors();
        ReadCsvAndMigrateToDatabase();
    }


    private void ReadCsvAndMigrateToDatabase()
    {
        // TODO move file path and csv files
        var fingerprints = CsvUtility.GetNetworkFingerprintsFromFolder(@"D:\sleeptik\Downloads\wifi\wifi\csv\kindle");
        var networkService = _provider.GetService<INetworkService>();
        var locationService = _provider.GetService<ILocationService>();

        MigrationUtility.NetworkFingerprintsToDatabase(fingerprints, networkService, locationService);
    }

    private void LoadFloors()
    {
        var mapPaths = new[]
        {
            "Maps\\1.png",
            "Maps\\2.png",
            "Maps\\3.png",
            "Maps\\4.png"
        };

        var fileTask = FileSystem.Current.OpenAppPackageFileAsync(mapPaths[0]);
        fileTask.Wait();

        var stream = fileTask.Result;

        mapImage.Source = ImageSource.FromStream(() => stream);
    }

    private void floorsPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker)
            mapImage.Source = ((MapInfo)picker.SelectedItem).FullPath;
    }
}

public class MapInfo
{
    public string Name { get; set; }
    public string FullPath { get; set; }

    public override string ToString() => Name;
}