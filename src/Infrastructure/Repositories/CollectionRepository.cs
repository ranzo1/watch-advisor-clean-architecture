using Domain.Collections;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class CollectionRepository(ApplicationDbContext context) : ICollectionRepository
{
    public Task<CollectionItem?> GetByUserAndWatchAsync(
        Guid userId,
        Guid watchId,
        CancellationToken cancellationToken = default) =>
        context.CollectionItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.WatchId == watchId, cancellationToken);

    public async Task<IReadOnlyList<CollectionItem>> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default) =>
        await context.CollectionItems
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.AddedAt)
            .ToListAsync(cancellationToken);

    public Task<bool> ExistsAsync(
        Guid userId,
        Guid watchId,
        CancellationToken cancellationToken = default) =>
        context.CollectionItems
            .AnyAsync(c => c.UserId == userId && c.WatchId == watchId, cancellationToken);

    public void Add(CollectionItem item) => context.CollectionItems.Add(item);

    public void Remove(CollectionItem item) => context.CollectionItems.Remove(item);
}
