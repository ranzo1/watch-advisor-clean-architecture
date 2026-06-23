using Application.Abstractions.Messaging;

namespace Application.Collections.AddToCollection;

public sealed record AddToCollectionCommand(Guid WatchId, string? Notes) : ICommand<Guid>;
