namespace ImiknWifiNavigationApp.IWNA.EF.Models
{
    public partial class Location
    {
        public Location()
        {
            ApLocations = new HashSet<ApLocation>();
            PointAs = new HashSet<Location>();
            PointBs = new HashSet<Location>();
        }

        public long LocationId { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public long Floor { get; set; }

        public virtual ICollection<ApLocation> ApLocations { get; set; }

        public virtual ICollection<Location> PointAs { get; set; }
        public virtual ICollection<Location> PointBs { get; set; }
    }
}
