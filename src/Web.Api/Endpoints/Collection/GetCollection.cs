using Application.Collections.GetCollection;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Collection;

internal sealed class GetCollection : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("collection", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCollectionQuery();

            Result<IReadOnlyList<CollectionItemResponse>> result =
                await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Collection);
    }
}
