using ImiknWifiNavigationApp.IWNA.EF.Services;
using Location = ImiknWifiNavigationApp.IWNA.EF.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.ML;

public class SimpleOutput
{
    public long LocationId { get; init; }
    public double Probability { get; init; }
    public Location LinkedLocation { get; private set; }

    public void LinkLocationUsingService(ILocationService locationService)
    {
        LinkedLocation = locationService.GetLocation(LocationId);
    }
}