using ImiknWifiNavigationApp.IWNA.Database.Models;
using Microsoft.EntityFrameworkCore;
using Location = ImiknWifiNavigationApp.IWNA.Database.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.Database.Services;

public class LocationService : ILocationService
{
    public LocationService(IwnaContext context)
    {
        Context = context;
    }

    private IwnaContext Context { get; }

    public Location AddLocation(Location location)
    {
        var entity = GetLocation(location);

        if (entity is not null)
            return entity;

        entity = Context.Locations.Add(new()
        {
            PosX = location.PosX,
            PosY = location.PosY,
            Floor = location.Floor
        }).Entity;

        Context.SaveChanges();

        return entity;
    }

    public Location AddLocation(double x, double y, int floor)
    {
        return AddLocation(new()
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

    public Location GetLocationAsNoTracking(long locationId)
    {
        return Context.Locations.FirstOrDefault(location => location.LocationId == locationId);
    }

    public Location GetLocation(Location location)
    {
        return GetLocation(location.LocationId);
    }

    public Location GetLocationAsNoTracking(Location location)
    {
        return GetLocationAsNoTracking(location.LocationId);
    }

    public List<Location> GetAllLocations()
    {
        return Context.Locations.ToList();
    }

    public List<Location> GetAllLocationsAsNoTracking()
    {
        return Context.Locations.AsNoTracking().ToList();
    }

    public List<Location> GetLocationsByFloor(int floor)
    {
        return Context.Locations
            .Where(location => location.Floor == floor)
            .ToList();
    }

    public List<Location> GetLocationsByFloorAsNoTracking(int floor)
    {
        return Context.Locations
            .AsNoTracking()
            .Where(location => location.Floor == floor)
            .ToList();
    }
}