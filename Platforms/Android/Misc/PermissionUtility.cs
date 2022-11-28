namespace ImiknWifiNavigationApp.IWNA.Misc;

public static class PermissionUtility
{
    public static async Task<bool> RequestWifiScanningPermissions()
    {
        return
            await Permissions.RequestAsync<Permissions.NetworkState>() == PermissionStatus.Granted
            && await Permissions.RequestAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Granted
            && await Permissions.RequestAsync<Permissions.LocationAlways>() == PermissionStatus.Granted;
    }

    public static async Task<bool> RequestStorageReadWritePermissions()
    {
        return
            await Permissions.RequestAsync<Permissions.StorageRead>() == PermissionStatus.Granted
            && await Permissions.RequestAsync<Permissions.StorageWrite>() == PermissionStatus.Granted;
    }
}