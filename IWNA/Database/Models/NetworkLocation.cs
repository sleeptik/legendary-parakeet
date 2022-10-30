namespace ImiknWifiNavigationApp.IWNA.Database.Models
{
    public partial class NetworkLocation
    {
        public long NetworkId { get; set; }
        public long LocationId { get; set; }
        public double SignalStrength { get; set; }

        public virtual Location Location { get; set; } = null!;
        public virtual Network Network { get; set; } = null!;
    }
}
