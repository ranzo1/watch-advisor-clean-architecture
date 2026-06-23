using Application.Collections.RemoveFromCollection;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Collection;

internal sealed class RemoveFromCollection : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("collection/{watchId:guid}", async (
            Guid watchId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RemoveFromCollectionCommand(watchId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Collection);
    }
}
