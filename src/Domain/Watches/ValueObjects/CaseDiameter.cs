using Domain.Shared.Services;
using Domain.Watches.Errors;
using SharedKernel;

namespace Domain.Watches.ValueObjects;

public sealed record CaseDiameter
{
    public const decimal MinMm = 20m;
    public const decimal MaxMm = 60m;

    private CaseDiameter(decimal mm) => Mm = mm;

    private CaseDiameter() { }

    public decimal Mm { get; private set; }

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
