namespace ImiknWifiNavigationApp.IWNA.EF.Models
{
    public partial class ApLocation
    {
        public long AccessPointId { get; set; }
        public long LocationId { get; set; }
        public long SignalStrength { get; set; }

        public virtual AccessPoint AccessPoint { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
    }
}
