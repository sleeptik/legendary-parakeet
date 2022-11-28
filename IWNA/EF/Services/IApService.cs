using ImiknWifiNavigationApp.IWNA.EF.Models;
using Location = ImiknWifiNavigationApp.IWNA.EF.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.EF.Services;

public interface IApService
{
    public AccessPoint AddAccessPoint(AccessPoint accessPoint);
    public AccessPoint AddAccessPoint(string ssid, string bssid);

    public ApLocation AddNetworkToLocation(ApLocation apLocation);
    public ApLocation AddNetworkToLocation(AccessPoint accessPoint, Location location, long signalStrength);

    public AccessPoint GetAccessPoint(AccessPoint accessPoint);
    public AccessPoint GetAccessPoint(string ssid, string bssid);

    public ApLocation GetApLocation(ApLocation apLocation);
    public ApLocation GetApLocation(AccessPoint accessPoint, Location location, long signalStrength);

    public List<ApLocation> GetAllApLocations();
}