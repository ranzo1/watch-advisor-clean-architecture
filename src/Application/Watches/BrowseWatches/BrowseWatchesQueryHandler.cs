using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.Watches.Enums;
using Domain.Watches.ValueObjects;
using SharedKernel;
using System.Data;

namespace Application.Watches.BrowseWatches;

internal sealed class BrowseWatchesQueryHandler(IDbConnectionFactory dbConnectionFactory) :
    IQueryHandler<BrowseWatchesQuery, IReadOnlyList<WatchResponse>>
{
    public async Task<Result<IReadOnlyList<WatchResponse>>> Handle(
        BrowseWatchesQuery request,
        CancellationToken cancellationToken)
    {
        if (request.MinPrice.HasValue || request.MaxPrice.HasValue)
        {
            Result<PriceRange> priceRangeResult = PriceRange.Create(
                request.MinPrice ?? 0,
                request.MaxPrice ?? decimal.MaxValue,
                request.Currency);

            if (priceRangeResult.IsFailure)
            {
                return Result.Failure<IReadOnlyList<WatchResponse>>(priceRangeResult.Error);
            }
        }

        const string sql =
            """
            SELECT
                w.id                AS Id,
                w.brand             AS Brand,
                w.model             AS Model,
                w.reference_number  AS ReferenceNumber,
                w.case_diameter_mm  AS CaseDiameterMm,
                w.case_thickness_mm AS CaseThicknessMm,
                w.lug_width_mm      AS LugWidthMm,
                w.lug_to_lug_mm     AS LugToLugMm,
                w.style             AS Style,
                w.movement          AS Movement,
                w.occasion          AS Occasion,
                w.price_eur         AS PriceEur,
                w.dial_color        AS DialColor,
                w.case_material     AS CaseMaterial,
                w.bracelet_type     AS BraceletType,
                w.image_url         AS ImageUrl,
                w.description       AS Description
            FROM watches w
            WHERE (@Brand IS NULL OR w.brand ILIKE @Brand)
              AND (@Style IS NULL OR w.style = @Style)
              AND (@Movement IS NULL OR w.movement = @Movement)
              AND (@MinPrice IS NULL OR w.price_eur >= @MinPrice)
              AND (@MaxPrice IS NULL OR w.price_eur <= @MaxPrice)
            """;

        using IDbConnection connection = dbConnectionFactory.GetOpenConnection();

        IEnumerable<WatchRow> rows = await connection.QueryAsync<WatchRow>(
            sql,
            new
            {
                request.Brand,
                Style = (int?)request.Style,
                Movement = (int?)request.Movement,
                request.MinPrice,
                request.MaxPrice
            });

        IReadOnlyList<WatchResponse> response = rows
            .Select(r => new WatchResponse(
                r.Id,
                r.Brand,
                r.Model,
                r.ReferenceNumber,
                r.CaseDiameterMm,
                r.CaseThicknessMm,
                r.LugWidthMm,
                r.LugToLugMm,
                r.Style,
                r.Movement,
                r.Occasion,
                r.PriceEur,
                r.DialColor,
                r.CaseMaterial,
                r.BraceletType,
                new Uri(r.ImageUrl),
                r.Description))
            .ToList();

        return Result.Success<IReadOnlyList<WatchResponse>>(response);
    }

    private sealed record WatchRow(
        Guid Id,
        string Brand,
        string Model,
        string ReferenceNumber,
        decimal CaseDiameterMm,
        decimal CaseThicknessMm,
        decimal LugWidthMm,
        decimal LugToLugMm,
        WatchStyle Style,
        MovementType Movement,
        WatchOccasion Occasion,
        decimal PriceEur,
        string DialColor,
        CaseMaterial CaseMaterial,
        BraceletType BraceletType,
        string ImageUrl,
        string? Description);
}
