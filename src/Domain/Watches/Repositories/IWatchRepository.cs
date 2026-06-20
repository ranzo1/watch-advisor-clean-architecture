using Domain.Watches.Entities;
using Domain.Watches.ValueObjects;

namespace Domain.Watches.Repositories;

public interface IWatchRepository
{
    Task<WatchAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WatchAggregate>> BrowseAsync(WatchFilter filter, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
