using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Collections;
using Domain.Watches;
using SharedKernel;

namespace Application.Collections.AddToCollection;

internal sealed class AddToCollectionCommandHandler(
    ICollectionRepository collectionRepository,
    IWatchRepository watchRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork) : ICommandHandler<AddToCollectionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        AddToCollectionCommand command,
        CancellationToken cancellationToken)
    {
        bool watchExists = await watchRepository.ExistsAsync(command.WatchId, cancellationToken);
        if (!watchExists)
        {
            return Result.Failure<Guid>(CollectionErrors.WatchNotFound(command.WatchId));
        }

        bool alreadyInCollection = await collectionRepository.ExistsAsync(
            userContext.UserId,
            command.WatchId,
            cancellationToken);

        if (alreadyInCollection)
        {
            return Result.Failure<Guid>(CollectionErrors.WatchAlreadyInCollection);
        }

        var item = CollectionItem.Create(userContext.UserId, command.WatchId, command.Notes);

        collectionRepository.Add(item);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
