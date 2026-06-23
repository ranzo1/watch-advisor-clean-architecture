using Application.Collections.AddToCollection;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Collection;

internal sealed class AddToCollection : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("collection", async (
            AddToCollectionCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(
                id => Results.Created($"collection/{id}", new { id }),
                CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Collection);
    }
}
