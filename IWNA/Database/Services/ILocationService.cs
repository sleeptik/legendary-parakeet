using ImiknWifiNavigationApp.IWNA.Database.Models;
using Location = ImiknWifiNavigationApp.IWNA.Database.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.Database.Services;

public interface ILocationService
{
    public Location AddLocation(Location location);
    public Location AddLocation(double x, double y, int floor);

    public Location GetLocation(long locationId);
    public Location GetLocationAsNoTracking(long locationId);
    public Location GetLocation(Location location);
    public Location GetLocationAsNoTracking(Location location);
    
    public List<Location> GetAllLocations();
    public List<Location> GetAllLocationsAsNoTracking();
    public List<Location> GetLocationsByFloor(int floor);
    public List<Location> GetLocationsByFloorAsNoTracking(int floor);
}