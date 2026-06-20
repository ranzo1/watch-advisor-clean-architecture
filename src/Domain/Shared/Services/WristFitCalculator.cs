namespace Domain.Shared.Services;

public static class WristFitCalculator
{
    public static (decimal Min, decimal Max) GetRecommendedDiameterRange(decimal wristCm) =>
        wristCm switch
        {
            < 15m => (34m, 38m),
            < 16m => (36m, 40m),
            < 17m => (38m, 42m),
            < 18m => (40m, 44m),
            < 19m => (42m, 46m),
            _ => (44m, 50m)
        };
}
