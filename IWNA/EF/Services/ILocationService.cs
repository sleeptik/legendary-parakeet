using Location = ImiknWifiNavigationApp.IWNA.EF.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.EF.Services;

public interface ILocationService
{
    public Location AddLocation(Location location);
    public Location AddLocation(double x, double y, long floor);

    public Location GetLocation(long locationId);
    public Location GetLocation(Location location);
    public Location GetLocation(double x, double y, long floor);
    public List<Location> GetFloorLocations(long floor);
    public void LinkLocations(Location locationA, Location locationB);
}