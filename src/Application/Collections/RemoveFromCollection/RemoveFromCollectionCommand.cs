using Application.Abstractions.Messaging;

namespace Application.Collections.RemoveFromCollection;

public sealed record RemoveFromCollectionCommand(Guid WatchId) : ICommand;
