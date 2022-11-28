namespace ImiknWifiNavigationApp.IWNA.EF.Models
{
    public partial class AccessPoint
    {
        public AccessPoint()
        {
            ApLocations = new HashSet<ApLocation>();
        }

        public long AccessPointId { get; set; }
        public string Ssid { get; set; } = null!;
        public string Bssid { get; set; } = null!;

        public virtual ICollection<ApLocation> ApLocations { get; set; }
    }
}
