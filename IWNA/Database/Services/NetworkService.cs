// ReSharper disable RedundantNameQualifier

using ImiknWifiNavigationApp.IWNA.Database.Models;
using Microsoft.EntityFrameworkCore;
using Location = ImiknWifiNavigationApp.IWNA.Database.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.Database.Services;

public class NetworkService : INetworkService
{
    public NetworkService(IwnaContext context)
    {
        Context = context;
    }

    private IwnaContext Context { get; }

    public Models.Network AddNetwork(Models.Network network)
    {
        var entity = GetNetwork(network);

        if (entity is not null)
            return entity;

        entity = Context.Networks.Add(new()
        {
            Bssid = network.Bssid,
            Ssid = network.Ssid
        }).Entity;

        Context.SaveChanges();

        return entity;
    }

    public Models.Network AddNetwork(string bssid, string ssid)
    {
        return AddNetwork(new()
        {
            Bssid = bssid,
            Ssid = ssid
        });
    }

    public NetworkLocation AddNetworkToLocation(NetworkLocation networkLocation)
    {
        var entity = Context.NetworkLocations.Add(networkLocation).Entity;

        Context.SaveChanges();

        return entity;
    }

    public NetworkLocation AddNetworkToLocation(Models.Network network, Location location, int signalStrength)
    {
        return AddNetworkToLocation(new NetworkLocation
        {
            Location = location,
            Network = network,
            SignalStrength = signalStrength
        });
    }

    public Models.Network GetNetwork(long networkId)
    {
        return Context.Networks.Find(networkId);
    }

    public Models.Network GetNetworkAsNoTracking(long networkId)
    {
        return Context.Networks.FirstOrDefault(network => network.NetworkId == networkId);
    }

    public Models.Network GetNetwork(Models.Network network)
    {
        return GetNetwork(network.NetworkId);
    }

    public Models.Network GetNetworkAsNoTracking(Models.Network network)
    {
        return GetNetworkAsNoTracking(network.NetworkId);
    }

    public List<Models.Network> GetAllNetworks()
    {
        return Context.Networks.ToList();
    }

    public List<Models.Network> GetAllNetworksAsNoTracking()
    {
        return Context.Networks.AsNoTracking().ToList();
    }

    public List<Models.Network> GetNetworksByLocation(Location location)
    {
        return Context.NetworkLocations
            .Include(networkLocation => networkLocation.Network)
            .Where(networkLocation => networkLocation.Location == location)
            .Select(networkLocation => networkLocation.Network)
            .ToList();
    }

    public List<Models.Network> GetNetworksByLocationAsNoTracking(Location location)
    {
        return Context.NetworkLocations
            .AsNoTracking()
            .Include(networkLocation => networkLocation.Network)
            .Where(networkLocation => networkLocation.Location == location)
            .Select(networkLocation => networkLocation.Network)
            .ToList();
    }

    public List<Models.Network> GetNetworksByFloor(int floor)
    {
        return Context.NetworkLocations
            .Where(location => location.Location.Floor == floor)
            .Select(location => location.Network)
            .ToList();
    }

    public List<Models.Network> GetNetworksByFloorAsNoTracking(int floor)
    {
        return Context.NetworkLocations
            .AsNoTracking()
            .Where(location => location.Location.Floor == floor)
            .Select(location => location.Network)
            .ToList();
    }
}