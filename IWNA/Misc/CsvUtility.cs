using System.Text;
using ImiknWifiNavigationApp.IWNA.EF.Models;
using ImiknWifiNavigationApp.IWNA.Misc.Models;

namespace ImiknWifiNavigationApp.IWNA.Misc;

public static class CsvUtility
{
    public static List<NetworkFingerprint> ReadAllFingerprintsFromCsvFolder(string folderPath)
    {
        var csvFileNames = Directory.GetFiles(folderPath).Where(file => file.EndsWith(".csv"));

        var fingerprints = new List<NetworkFingerprint>();

        foreach (var csvFileName in csvFileNames)
        {
            using var streamReader = new StreamReader(csvFileName);

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
        }

        return fingerprints;
    }

    public static void WriteNetworkLocationsToCsv(string path, List<ApLocation> networkLocations)
    {
        using var streamWriter = new StreamWriter(path, false);

        var builder = new StringBuilder();
        foreach (var networkLocation in networkLocations)
        {
            builder
                .Append($"{networkLocation.AccessPointId},")
                .Append($"{networkLocation.LocationId},")
                .AppendLine($"{networkLocation.SignalStrength}");
        }

        streamWriter.Write(builder.ToString());
    }
}