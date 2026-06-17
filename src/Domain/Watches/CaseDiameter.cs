using SharedKernel;

namespace Domain.Watches;

public sealed record CaseDiameter
{
    public const decimal MinMm = 20m;
    public const decimal MaxMm = 60m;

    private CaseDiameter(decimal mm) => Mm = mm;

    public decimal Mm { get; }

    public static Result<CaseDiameter> Create(decimal mm)
    {
        if (mm is < MinMm or > MaxMm)
        {
            return Result.Failure<CaseDiameter>(WatchErrors.InvalidCaseDiameter);
        }

        return new CaseDiameter(mm);
    }

    public bool FitsWrist(decimal wristCircumferenceCm)
    {
        (decimal min, decimal max) = WristFitCalculator.GetRecommendedDiameterRange(wristCircumferenceCm);
        return Mm >= min && Mm <= max;
    }
}
