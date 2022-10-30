// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace ImiknWifiNavigationApp.IWNA.Misc;

public class NetworkFingerprint
{
    public double X { get; set; }
    public double Y { get; set; }
    public int Floor { get; set; }
    public string Ssid { get; set; }
    public string Bssid { get; set; }
    public int SignalStrength { get; set; }

    public override string ToString()
    {
        return $"Position: ({X}, {Y}) {Floor}\nSsid: {Ssid}\nBssid: {Bssid}\nSignal strength: {SignalStrength}";
    }
}