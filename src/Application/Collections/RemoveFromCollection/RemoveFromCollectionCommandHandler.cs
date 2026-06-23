using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Collections;
using SharedKernel;

namespace Application.Collections.RemoveFromCollection;

internal sealed class RemoveFromCollectionCommandHandler(
    ICollectionRepository collectionRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveFromCollectionCommand>
{
    public async Task<Result> Handle(
        RemoveFromCollectionCommand command,
        CancellationToken cancellationToken)
    {
        CollectionItem? item = await collectionRepository.GetByUserAndWatchAsync(
            userContext.UserId,
            command.WatchId,
            cancellationToken);

        if (item is null)
        {
            return Result.Failure(CollectionErrors.ItemNotFound(command.WatchId));
        }

        collectionRepository.Remove(item);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
