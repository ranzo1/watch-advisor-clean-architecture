using Domain.Watches.Errors;
using SharedKernel;

namespace Domain.Watches.ValueObjects;

public sealed record WatchDimensions
{
    private WatchDimensions(
        CaseDiameter caseDiameter,
        decimal caseThicknessMm,
        decimal lugWidthMm,
        decimal lugToLugMm)
    {
        CaseDiameter = caseDiameter;
        CaseThicknessMm = caseThicknessMm;
        LugWidthMm = lugWidthMm;
        LugToLugMm = lugToLugMm;
    }

    public CaseDiameter CaseDiameter { get; }
    public decimal CaseThicknessMm { get; }
    public decimal LugWidthMm { get; }
    public decimal LugToLugMm { get; }

    public static Result<WatchDimensions> Create(
        decimal caseDiameterMm,
        decimal caseThicknessMm,
        decimal lugWidthMm,
        decimal lugToLugMm)
    {
        Result<CaseDiameter> diameterResult = CaseDiameter.Create(caseDiameterMm);
        if (diameterResult.IsFailure)
        {
            return Result.Failure<WatchDimensions>(diameterResult.Error);
        }

        Result thicknessCheck = ValidatePositive(caseThicknessMm, WatchErrors.InvalidCaseThickness);
        if (thicknessCheck.IsFailure)
        {
            return Result.Failure<WatchDimensions>(thicknessCheck.Error);
        }

        Result lugWidthCheck = ValidatePositive(lugWidthMm, WatchErrors.InvalidLugWidth);
        if (lugWidthCheck.IsFailure)
        {
            return Result.Failure<WatchDimensions>(lugWidthCheck.Error);
        }

        Result lugToLugCheck = ValidatePositive(lugToLugMm, WatchErrors.InvalidLugToLug);
        if (lugToLugCheck.IsFailure)
        {
            return Result.Failure<WatchDimensions>(lugToLugCheck.Error);
        }

        return new WatchDimensions(diameterResult.Value, caseThicknessMm, lugWidthMm, lugToLugMm);
    }

    public bool FitsWrist(decimal wristCircumferenceCm) =>
        CaseDiameter.FitsWrist(wristCircumferenceCm);

    private static Result ValidatePositive(decimal value, Error error) =>
        value <= 0 ? Result.Failure(error) : Result.Success();
}
