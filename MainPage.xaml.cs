using ImiknWifiNavigationApp.IWNA.EF.Services;
using ImiknWifiNavigationApp.IWNA.Misc;
using ImiknWifiNavigationApp.IWNA.ML;

namespace ImiknWifiNavigationApp;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _provider;

    public MainPage(IServiceProvider provider)
    {
        _provider = provider;
        InitializeComponent();
        LoadFloors();


#if ANDROID
        if (!ResourceTransferUtility.IsDatabaseCopied())
            ResourceTransferUtility.CopyDatabaseToWritableDirectory();
#endif
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

    private async void OnButtonClicked(object sender, EventArgs e)
    {
#if ANDROID
        if (!await PermissionUtility.RequestWifiScanningPermissions())
            return;
#endif

        var apService = _provider.GetService<IApService>();
        var locationService = _provider.GetService<ILocationService>();

        var locationsForInputs = apService!.GetAllApLocations()
            .Where(location => location.LocationId is 4 or 5);

        var inputs = locationsForInputs
            .Select(location => new SimpleInput
            {
                LocationId = location.LocationId,
                SignalStrength = location.SignalStrength
            }).ToList();

        var predictions = EasyPredictor.PredictLocationsWeighted(inputs);
        predictions.ForEach(output => output.LinkLocationUsingService(locationService));

        var x = 0.0;
        var y = 0.0;

        foreach (var prediction in predictions)
        {
            x += prediction.LinkedLocation.PosX * prediction.Probability;
            y += prediction.LinkedLocation.PosY * prediction.Probability;
        }
    }
}