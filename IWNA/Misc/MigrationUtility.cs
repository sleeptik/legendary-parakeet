// ReSharper disable RedundantNameQualifier

using ImiknWifiNavigationApp.IWNA.Database.Models;
using ImiknWifiNavigationApp.IWNA.Database.Services;
using Location = ImiknWifiNavigationApp.IWNA.Database.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.Misc;

public static class MigrationUtility
{
    public static void NetworkFingerprintsToDatabase(
        List<NetworkFingerprint> fingerprints,
        INetworkService networkService,
        ILocationService locationService)
    {
        foreach (var fingerprint in fingerprints)
        {
            var network = networkService.AddNetwork(new ImiknWifiNavigationApp.IWNA.Database.Models.Network
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

            networkService.AddNetworkToLocation(new NetworkLocation
            {
                Network = network,
                Location = location,
                SignalStrength = fingerprint.SignalStrength
            });
        }
    }
}