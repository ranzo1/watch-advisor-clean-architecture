using Domain.Watches.Enums;

namespace Application.Watches.BrowseWatches;

public sealed record WatchResponse(
    Guid Id,
    string Brand,
    string Model,
    string ReferenceNumber,
    decimal CaseDiameterMm,
    decimal CaseThicknessMm,
    decimal LugWidthMm,
    decimal LugToLugMm,
    WatchStyle Style,
    MovementType Movement,
    WatchOccasion Occasion,
    decimal PriceEur,
    string DialColor,
    CaseMaterial CaseMaterial,
    BraceletType BraceletType,
    Uri ImageUrl,
    string? Description);
