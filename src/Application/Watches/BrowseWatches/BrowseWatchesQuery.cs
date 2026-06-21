using Application.Abstractions.Messaging;
using Domain.Watches.Enums;

namespace Application.Watches.BrowseWatches;

public sealed record BrowseWatchesQuery(
    string? Brand,
    WatchStyle? Style,
    MovementType? Movement,
    decimal? MinPrice,
    decimal? MaxPrice,
    string? Currency) : IQuery<IReadOnlyList<WatchResponse>>;
