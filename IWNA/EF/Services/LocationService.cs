using ImiknWifiNavigationApp.IWNA.EF.Models;
using Location = ImiknWifiNavigationApp.IWNA.EF.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.EF.Services;

public class LocationService : ILocationService
{
    private const double SearchTolerance = 0.01;

    public LocationService(IwnaContext context)
    {
        Context = context;
    }

    private IwnaContext Context { get; }


    public Location AddLocation(Location location)
    {
        var entity = GetLocation(location);

        if (entity != null)
            return entity;

        entity = Context.Locations.Add(location).Entity;
        Context.SaveChanges();

        return entity;
    }

    public Location AddLocation(double x, double y, long floor)
    {
        return AddLocation(new Location
        {
            PosX = x,
            PosY = y,
            Floor = floor
        });
    }

    public Location GetLocation(long locationId)
    {
        return Context.Locations.Find(locationId);
    }

    public Location GetLocation(Location location)
    {
        return Context.Locations
            .FirstOrDefault(searchingLoc =>
                Math.Abs(searchingLoc.PosX - location.PosX) < SearchTolerance
                && Math.Abs(searchingLoc.PosY - location.PosY) < SearchTolerance
                && searchingLoc.Floor == location.Floor);
    }

    public Location GetLocation(double x, double y, long floor)
    {
        return GetLocation(new Location
        {
            PosX = x,
            PosY = y,
            Floor = floor
        });
    }

    public List<Location> GetFloorLocations(long floor)
    {
        return Context.Locations
            .Where(location => location.Floor == floor)
            .ToList();
    }

    public void LinkLocations(Location locationA, Location locationB)
    {
        locationA = GetLocation(locationA);
        locationB = GetLocation(locationB);

        if (!locationA.PointBs.Contains(locationB) && locationB.PointAs.Contains(locationA))
        {
            locationA.PointBs.Add(locationB);
            locationB.PointAs.Add(locationA);
        }
        
        Context.SaveChanges();
    }
}