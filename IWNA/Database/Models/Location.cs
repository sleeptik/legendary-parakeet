namespace ImiknWifiNavigationApp.IWNA.Database.Models
{
    public partial class Location
    {
        public Location()
        {
            NetworkLocations = new HashSet<NetworkLocation>();
            EndNodes = new HashSet<Location>();
            StartNodes = new HashSet<Location>();
        }

        public long LocationId { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public long Floor { get; set; }

        public virtual ICollection<NetworkLocation> NetworkLocations { get; set; }

        public virtual ICollection<Location> EndNodes { get; set; }
        public virtual ICollection<Location> StartNodes { get; set; }
    }
}
