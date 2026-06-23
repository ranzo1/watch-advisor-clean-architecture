using Application.Abstractions.Messaging;

namespace Application.Collections.GetCollection;

public sealed record GetCollectionQuery : IQuery<IReadOnlyList<CollectionItemResponse>>;
