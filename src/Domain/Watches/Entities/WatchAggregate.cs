using Domain.Watches.ValueObjects;
using SharedKernel;

namespace Domain.Watches;

public sealed class WatchAggregate : Entity
{
    private WatchAggregate(
        Guid id,
        Brand brand,
        Model model,
        ReferenceNumber referenceNumber,
        WatchDimensions dimensions,
        WatchStyle style,
        MovementType movement,
        WatchOccasion occasion,
        Price price,
        DialColor dialColor,
        CaseMaterial caseMaterial,
        BraceletType braceletType,
        Uri imageUrl,
        Description? description)
        : base(id)
    {
        Brand = brand;
        Model = model;
        ReferenceNumber = referenceNumber;
        Dimensions = dimensions;
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

    private WatchAggregate()
    {
    }

    public Brand Brand { get; private set; } = null!;
    public Model Model { get; private set; } = null!;
    public ReferenceNumber ReferenceNumber { get; private set; }
    public WatchDimensions Dimensions { get; private set; } = null!;
    public WatchStyle Style { get; private set; }
    public MovementType Movement { get; private set; }
    public WatchOccasion Occasion { get; private set; }
    public Price Price { get; private set; } = null!;
    public DialColor DialColor { get; private set; }
    public CaseMaterial CaseMaterial { get; private set; }
    public BraceletType BraceletType { get; private set; }
    public Uri ImageUrl { get; private set; } = new Uri("about:blank");
    public Description? Description { get; private set; }

    public static Result<WatchAggregate> Create(
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
        BraceletType braceletType,
        Uri imageUrl,
        string? description)
    {
        Result<Brand> brandResult = Brand.Create(brand);
        if (brandResult.IsFailure)
        {
            return Result.Failure<WatchAggregate>(brandResult.Error);
        }

        Result<Model> modelResult = Model.Create(model);
        if (modelResult.IsFailure)
        {
            return Result.Failure<WatchAggregate>(modelResult.Error);
        }

        Result<ReferenceNumber> referenceNumberResult = ReferenceNumber.Create(referenceNumber);
        if (referenceNumberResult.IsFailure)
        {
            return Result.Failure<WatchAggregate>(referenceNumberResult.Error);
        }

        Result<WatchDimensions> dimensionsResult = WatchDimensions.Create(
            caseDiameterMm, caseThicknessMm, lugWidthMm, lugToLugMm);
        if (dimensionsResult.IsFailure)
        {
            return Result.Failure<WatchAggregate>(dimensionsResult.Error);
        }

        Result<Price> priceResult = Price.Create(priceEur);
        if (priceResult.IsFailure)
        {
            return Result.Failure<WatchAggregate>(priceResult.Error);
        }

        Result<DialColor> dialColorResult = DialColor.Create(dialColor);
        if (dialColorResult.IsFailure)
        {
            return Result.Failure<WatchAggregate>(dialColorResult.Error);
        }

        Description? watchDescription = string.IsNullOrWhiteSpace(description)
            ? null
            : new Description(description);

        return new WatchAggregate(
            Guid.NewGuid(),
            brandResult.Value,
            modelResult.Value,
            referenceNumberResult.Value,
            dimensionsResult.Value,
            style,
            movement,
            occasion,
            priceResult.Value,
            dialColorResult.Value,
            caseMaterial,
            braceletType,
            imageUrl,
            watchDescription);
    }

    public bool FitsWrist(decimal wristCircumferenceCm) =>
        Dimensions.FitsWrist(wristCircumferenceCm);

    public bool IsWithinBudget(decimal budgetEur) =>
        Price.IsWithinBudget(budgetEur);
}
