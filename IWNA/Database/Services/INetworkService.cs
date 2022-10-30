// ReSharper disable RedundantNameQualifier

using ImiknWifiNavigationApp.IWNA.Database.Models;
using Location = ImiknWifiNavigationApp.IWNA.Database.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.Database.Services;

public interface INetworkService
{
    public Models.Network AddNetwork(Models.Network network);
    public Models.Network AddNetwork(string bssid, string ssid);

    public NetworkLocation AddNetworkToLocation(NetworkLocation networkLocation);
    public NetworkLocation AddNetworkToLocation(Models.Network network, Location location, int signalStrength);

    public Models.Network GetNetwork(long networkId);
    public Models.Network GetNetworkAsNoTracking(long networkId);
    public Models.Network GetNetwork(Models.Network network);
    public Models.Network GetNetworkAsNoTracking(Models.Network network);

    public List<Models.Network> GetAllNetworks();
    public List<Models.Network> GetAllNetworksAsNoTracking();
    public List<Models.Network> GetNetworksByLocation(Location location);
    public List<Models.Network> GetNetworksByLocationAsNoTracking(Location location);
    public List<Models.Network> GetNetworksByFloor(int floor);
    public List<Models.Network> GetNetworksByFloorAsNoTracking(int floor);
}