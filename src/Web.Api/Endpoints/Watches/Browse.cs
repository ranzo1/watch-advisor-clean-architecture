using Application.Watches.BrowseWatches;
using Domain.Watches.Enums;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Watches;

internal sealed class Browse : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("watches", async (
            string? brand,
            WatchStyle? style,
            MovementType? movement,
            decimal? minPrice,
            decimal? maxPrice,
            string? currency,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new BrowseWatchesQuery(brand, style, movement, minPrice, maxPrice, currency);

            Result<IReadOnlyList<WatchResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Watches);
    }
}
