using Domain.Watches.Enums;

namespace Domain.Watches.ValueObjects;

public sealed record WatchFilter(
    string? Brand = null,
    WatchStyle? Style = null,
    MovementType? Movement = null,
    PriceRange? PriceRange = null);
