using ImiknWifiNavigationApp.IWNA.ML.Models;

namespace ImiknWifiNavigationApp.IWNA.ML;

public static class EasyPredictor
{
    private const double WeightedThreshold = 0.2;

    public static SimpleOutput PredictLocationMostFrequent(List<SimpleInput> inputs)
    {
        var modelOutputs = GetModelOutputs(inputs);

        var grouped = modelOutputs
            .Select(output => Convert.ToInt32(output.Prediction))
            .GroupBy(i => i)
            .OrderByDescending(group => group.Count())
            .ToList();

        return new SimpleOutput
        {
            LocationId = grouped.First().Key,
            Probability = (double)grouped.First().Count() / modelOutputs.Count
        };
    }


    public static List<SimpleOutput> PredictLocationsWeighted(List<SimpleInput> inputs)
    {
        var modelOutputs = GetModelOutputs(inputs);

        var grouped = modelOutputs
            .Select(output => Convert.ToInt32(output.Prediction))
            .GroupBy(i => i)
            .Where(group => group.Count() >= modelOutputs.Count * WeightedThreshold)
            .OrderByDescending(group => group.Count())
            .ToList();

        return grouped
            .Select(group => new SimpleOutput
            {
                LocationId = group.Key,
                Probability = (double)group.Count() / grouped.Count
            }).ToList();
    }

    private static List<WifiFingerprintModel.ModelOutput> GetModelOutputs(List<SimpleInput> inputs)
    {
        var modelInputs = inputs
            .Select(input => new WifiFingerprintModel.ModelInput
            {
                Col0 = input.LocationId,
                Col2 = input.SignalStrength
            });

        var modelOutputs = modelInputs
            .Select(input => WifiFingerprintModel.Predict(input))
            .ToList();

        return modelOutputs;
    }
}