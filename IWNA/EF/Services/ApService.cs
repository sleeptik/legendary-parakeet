using ImiknWifiNavigationApp.IWNA.EF.Models;
using Location = ImiknWifiNavigationApp.IWNA.EF.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.EF.Services;

public class ApService : IApService
{
    private const double SearchTolerance = 0.01;

    public ApService(IwnaContext context)
    {
        Context = context;
    }

    private IwnaContext Context { get; }

    public AccessPoint AddAccessPoint(AccessPoint accessPoint)
    {
        var entity = GetAccessPoint(accessPoint);

        if (entity != null)
            return entity;

        entity = Context.AccessPoints.Add(accessPoint).Entity;
        Context.SaveChanges();

        return entity;
    }

    public AccessPoint AddAccessPoint(string ssid, string bssid)
    {
        return AddAccessPoint(new AccessPoint
        {
            Ssid = ssid,
            Bssid = bssid
        });
    }

    public ApLocation AddNetworkToLocation(ApLocation apLocation)
    {
        var entity = GetApLocation(apLocation);

        if (entity != null)
            return entity;

        entity = Context.ApLocations.Add(apLocation).Entity;
        Context.SaveChanges();

        return entity;
    }

    public ApLocation AddNetworkToLocation(AccessPoint accessPoint, Location location, long signalStrength)
    {
        throw new NotImplementedException();
    }

    public AccessPoint GetAccessPoint(AccessPoint accessPoint)
    {
        return Context.AccessPoints
            .FirstOrDefault(searchingEntity =>
                searchingEntity.Ssid == accessPoint.Ssid
                && searchingEntity.Bssid == accessPoint.Bssid);
    }

    public AccessPoint GetAccessPoint(string ssid, string bssid)
    {
        return GetAccessPoint(new AccessPoint
        {
            Ssid = ssid,
            Bssid = bssid
        });
    }

    public ApLocation GetApLocation(ApLocation apLocation)
    {
        return Context.ApLocations
            .FirstOrDefault(searchingEntity =>
                searchingEntity.AccessPointId == apLocation.AccessPoint.AccessPointId
                && searchingEntity.LocationId == apLocation.Location.LocationId);
    }

    public ApLocation GetApLocation(AccessPoint accessPoint, Location location, long signalStrength)
    {
        return GetApLocation(new ApLocation
        {
            AccessPointId = accessPoint.AccessPointId,
            LocationId = location.LocationId,
            SignalStrength = signalStrength
        });
    }

    public List<ApLocation> GetAllApLocations()
    {
        return Context.ApLocations.ToList();
    }
}