namespace Domain.Collections;

public interface ICollectionRepository
{
    Task<CollectionItem?> GetByUserAndWatchAsync(
        Guid userId,
        Guid watchId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CollectionItem>> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Guid userId,
        Guid watchId,
        CancellationToken cancellationToken = default);

    void Add(CollectionItem item);

    void Remove(CollectionItem item);
}
