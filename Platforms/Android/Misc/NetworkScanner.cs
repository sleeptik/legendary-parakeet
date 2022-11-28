using Android.Content;
using Android.Net.Wifi;
using ImiknWifiNavigationApp.IWNA.Misc.Models;
using Application = Android.App.Application;

namespace ImiknWifiNavigationApp.IWNA.Misc;

public static class NetworkScanner
{
    private const int MaxScanTimeInSeconds = 12;
    private static readonly WifiManager _wifiManager;
    private static readonly WifiBroadcastReceiver _receiver;


    static NetworkScanner()
    {
        _wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
        _receiver = new WifiBroadcastReceiver();

        Application.Context.RegisterReceiver(_receiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
    }


    private static IList<NetworkFingerprint> FoundNetworks
    {
        get
        {
            return _receiver.ScanResults?
                .Select(result => new NetworkFingerprint
                {
                    Ssid = result.Ssid,
                    Bssid = result.Bssid,
                    SignalStrength = result.Level
                }).ToList();
        }
    }

    public static IList<NetworkFingerprint> ScanForWifiNetworks()
    {
        var timeoutTimer = new Timer(_ => _receiver.AutoResetEvent.Set(), null,
            TimeSpan.FromSeconds(MaxScanTimeInSeconds), Timeout.InfiniteTimeSpan);

        _wifiManager.StartScan();
        _receiver.AutoResetEvent.WaitOne();

        return FoundNetworks;
    }
}

internal class WifiBroadcastReceiver : BroadcastReceiver
{
    private static readonly WifiManager WifiManager;

    static WifiBroadcastReceiver()
    {
        var context = Application.Context;
        WifiManager = context.GetSystemService(Context.WifiService) as WifiManager;
    }

    public IList<ScanResult> ScanResults { get; private set; }
    public AutoResetEvent AutoResetEvent { get; } = new(false);


    public override void OnReceive(Context context, Intent intent)
    {
        if (intent.Action != WifiManager.ScanResultsAvailableAction)
            return;
        
        ScanResults = WifiManager.ScanResults;
        AutoResetEvent.Set();
    }
}