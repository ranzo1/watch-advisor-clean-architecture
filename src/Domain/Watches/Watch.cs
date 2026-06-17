using SharedKernel;

namespace Domain.Watches;

public sealed class Watch : Entity
{
    private Watch(
        Guid id,
        Brand brand,
        Model model,
        string referenceNumber,
        CaseDiameter caseDiameter,
        decimal caseThicknessMm,
        decimal lugWidthMm,
        decimal lugToLugMm,
        WatchStyle style,
        MovementType movement,
        WatchOccasion occasion,
        Price price,
        string dialColor,
        CaseMaterial caseMaterial,
        string braceletType,
        Uri imageUrl,
        string description)
        : base(id)
    {
        Brand = brand;
        Model = model;
        ReferenceNumber = referenceNumber;
        CaseDiameter = caseDiameter;
        CaseThicknessMm = caseThicknessMm;
        LugWidthMm = lugWidthMm;
        LugToLugMm = lugToLugMm;
        Style = style;
        Movement = movement;
        Occasion = occasion;
        Price = price;
        DialColor = dialColor;
        CaseMaterial = caseMaterial;
        BraceletType = braceletType;
        ImageUrl = imageUrl;
        Description = description;
    }

    private Watch()
    {
    }

    public Brand Brand { get; private set; } = null!;
    public Model Model { get; private set; } = null!;
    public string ReferenceNumber { get; private set; } = string.Empty;
    public CaseDiameter CaseDiameter { get; private set; } = null!;
    public decimal CaseThicknessMm { get; private set; }
    public decimal LugWidthMm { get; private set; }
    public decimal LugToLugMm { get; private set; }
    public WatchStyle Style { get; private set; }
    public MovementType Movement { get; private set; }
    public WatchOccasion Occasion { get; private set; }
    public Price Price { get; private set; } = null!;
    public string DialColor { get; private set; } = string.Empty;
    public CaseMaterial CaseMaterial { get; private set; }
    public string BraceletType { get; private set; } = string.Empty;
    public Uri ImageUrl { get; private set; } = new Uri("about:blank");
    public string Description { get; private set; } = string.Empty;

    public static Result<Watch> Create(
        string brand,
        string model,
        string referenceNumber,
        decimal caseDiameterMm,
        decimal caseThicknessMm,
        decimal lugWidthMm,
        decimal lugToLugMm,
        WatchStyle style,
        MovementType movement,
        WatchOccasion occasion,
        decimal priceEur,
        string dialColor,
        CaseMaterial caseMaterial,
        string braceletType,
        Uri imageUrl,
        string description)
    {
        Result<Brand> brandResult = Brand.Create(brand);
        if (brandResult.IsFailure)
        {
            return Result.Failure<Watch>(brandResult.Error);
        }

        Result<Model> modelResult = Model.Create(model);
        if (modelResult.IsFailure)
        {
            return Result.Failure<Watch>(modelResult.Error);
        }

        Result<CaseDiameter> diameterResult = CaseDiameter.Create(caseDiameterMm);
        if (diameterResult.IsFailure)
        {
            return Result.Failure<Watch>(diameterResult.Error);
        }

        Result<Price> priceResult = Price.Create(priceEur);
        if (priceResult.IsFailure)
        {
            return Result.Failure<Watch>(priceResult.Error);
        }

        return new Watch(
            Guid.NewGuid(),
            brandResult.Value,
            modelResult.Value,
            referenceNumber,
            diameterResult.Value,
            caseThicknessMm,
            lugWidthMm,
            lugToLugMm,
            style,
            movement,
            occasion,
            priceResult.Value,
            dialColor,
            caseMaterial,
            braceletType,
            imageUrl,
            description);
    }

    public bool FitsWrist(decimal wristCircumferenceCm) =>
        CaseDiameter.FitsWrist(wristCircumferenceCm);

    public bool IsWithinBudget(decimal budgetEur) =>
        Price.IsWithinBudget(budgetEur);
}
