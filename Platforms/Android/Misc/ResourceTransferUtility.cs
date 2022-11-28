using Java.Lang;

namespace ImiknWifiNavigationApp.IWNA.Misc;

public static class ResourceTransferUtility
{
    private const string ResourceDatabasePath = @"Database/iwna.sqlite3";

    public static readonly string ExtDatabasePath =
        @$"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/iwna.sqlite3";

    public static bool IsDatabaseCopied()
    {
        return File.Exists(ExtDatabasePath);
    }

    public static void CopyDatabaseToWritableDirectory()
    {
        var permissionsTask = PermissionUtility.RequestStorageReadWritePermissions();
        permissionsTask.Wait();

        if (!permissionsTask.Result)
            throw new SecurityException("Required storage permissions are missing");

        var fileTask = FileSystem.Current.OpenAppPackageFileAsync(ResourceDatabasePath);
        fileTask.Wait();

        using var stream = fileTask.Result;
        using var fileStream = new FileStream(ExtDatabasePath, FileMode.Create);
        
        stream.CopyTo(fileStream);
    }
}