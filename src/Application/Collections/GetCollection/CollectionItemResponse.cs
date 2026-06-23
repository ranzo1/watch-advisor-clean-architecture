namespace Application.Collections.GetCollection;

public sealed record CollectionItemResponse(
    Guid WatchId,
    string Brand,
    string Model,
    string ReferenceNumber,
    decimal PriceEur,
    string Style,
    string Movement,
    Uri ImageUrl,
    string? Notes,
    DateTime AddedAt);
