namespace ImiknWifiNavigationApp.IWNA.Misc;

public static class CsvUtility
{
    public static List<NetworkFingerprint> GetNetworkFingerprintsFromFolder(string csvFolder)
    {
        var fileEntries = Directory.GetFiles(csvFolder).Where(f => f.EndsWith(".csv"));
        var fingerprints = new List<NetworkFingerprint>();

        foreach (var fileEntry in fileEntries)
            fingerprints.AddRange(GetNetworkFingerprintsFromCsv(fileEntry));

        return fingerprints;
    }

    public static List<NetworkFingerprint> GetNetworkFingerprintsFromCsv(string csvPath)
    {
        var fingerprints = new List<NetworkFingerprint>();

        using var streamReader = new StreamReader(csvPath);
        while (!streamReader.EndOfStream)
        {
            var lineValues = streamReader.ReadLine()?.Split(',');

            if (lineValues is null || !lineValues[4].Contains("UTMN"))
                continue;

            fingerprints.Add(new NetworkFingerprint
            {
                X = Convert.ToDouble(lineValues[1]),
                Y = Convert.ToDouble(lineValues[2]),
                Floor = Convert.ToInt32(lineValues[3]),
                Ssid = lineValues[4],
                Bssid = lineValues[5],
                SignalStrength = Convert.ToInt32(lineValues[6])
            });
        }

        return fingerprints;
    }
}