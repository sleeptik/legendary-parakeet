namespace ImiknWifiNavigationApp.IWNA.Database.Models
{
    public partial class Network
    {
        public Network()
        {
            NetworkLocations = new HashSet<NetworkLocation>();
        }

        public long NetworkId { get; set; }
        public string Ssid { get; set; } = null!;
        public string Bssid { get; set; } = null!;

        public virtual ICollection<NetworkLocation> NetworkLocations { get; set; }
    }
}