using Domain.Watches;
using Domain.Watches.Entities;
using Domain.Watches.ValueObjects;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class WatchRepository(ApplicationDbContext context) : IWatchRepository
{
    public Task<WatchAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Watches.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

    public async Task<IReadOnlyList<WatchAggregate>> BrowseAsync(WatchFilter filter, CancellationToken cancellationToken = default)
    {
        IQueryable<WatchAggregate> query = context.Watches.AsQueryable();

        if (filter.Style.HasValue)
        {
            query = query.Where(w => w.Style == filter.Style.Value);
        }

        if (filter.Movement.HasValue)
        {
            query = query.Where(w => w.Movement == filter.Movement.Value);
        }

        if (filter.PriceRange is not null)
        {
            query = query.Where(w =>
                w.Price.Eur >= filter.PriceRange.Min &&
                w.Price.Eur <= filter.PriceRange.Max);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Watches.AnyAsync(w => w.Id == id, cancellationToken);
}
