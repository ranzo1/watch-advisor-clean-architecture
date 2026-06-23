using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;
using System.Data;

namespace Application.Collections.GetCollection;

internal sealed class GetCollectionQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IUserContext userContext) : IQueryHandler<GetCollectionQuery, IReadOnlyList<CollectionItemResponse>>
{
    public async Task<Result<IReadOnlyList<CollectionItemResponse>>> Handle(
        GetCollectionQuery request,
        CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                c.watch_id      AS WatchId,
                w.brand         AS Brand,
                w.model         AS Model,
                w.reference_number AS ReferenceNumber,
                w.price_eur     AS PriceEur,
                w.style         AS Style,
                w.movement      AS Movement,
                w.image_url     AS ImageUrl,
                c.notes         AS Notes,
                c.added_at      AS AddedAt
            FROM user_collections c
            JOIN watches w ON w.id = c.watch_id
            WHERE c.user_id = @UserId
            ORDER BY c.added_at DESC
            """;

        using IDbConnection connection = dbConnectionFactory.GetOpenConnection();

        IEnumerable<CollectionItemRow> rows = await connection.QueryAsync<CollectionItemRow>(
            sql,
            new { userContext.UserId });

        IReadOnlyList<CollectionItemResponse> response = rows
            .Select(r => new CollectionItemResponse(
                r.WatchId,
                r.Brand,
                r.Model,
                r.ReferenceNumber,
                r.PriceEur,
                r.Style,
                r.Movement,
                new Uri(r.ImageUrl),
                r.Notes,
                r.AddedAt))
            .ToList();

        return Result.Success<IReadOnlyList<CollectionItemResponse>>(response);
    }

#pragma warning disable S3459, S1144
    private sealed class CollectionItemRow
    {
        public Guid WatchId { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string ReferenceNumber { get; set; } = null!;
        public decimal PriceEur { get; set; }
        public string Style { get; set; } = null!;
        public string Movement { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime AddedAt { get; set; }
    }
#pragma warning restore S3459, S1144
}
