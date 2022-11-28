using ImiknWifiNavigationApp.IWNA.EF.Models;
using ImiknWifiNavigationApp.IWNA.EF.Services;
using ImiknWifiNavigationApp.IWNA.Misc.Models;
using Location = ImiknWifiNavigationApp.IWNA.EF.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.Misc;

public static class MigrationUtility
{
    public static void AddNetworkFingerprintsToDatabase(
        List<NetworkFingerprint> fingerprints,
        IApService apService,
        ILocationService locationService)
    {
        foreach (var fingerprint in fingerprints)
        {
            var accessPoint = apService.AddAccessPoint(new AccessPoint
            {
                Bssid = fingerprint.Bssid,
                Ssid = fingerprint.Ssid
            });

            var location = locationService.AddLocation(new Location
            {
                PosX = fingerprint.X,
                PosY = fingerprint.Y,
                Floor = fingerprint.Floor
            });

            apService.AddNetworkToLocation(new ApLocation
            {
                AccessPoint = accessPoint,
                Location = location,
                AccessPointId = accessPoint.AccessPointId,
                LocationId = location.LocationId,
                SignalStrength = fingerprint.SignalStrength
            });
        }
    }
}