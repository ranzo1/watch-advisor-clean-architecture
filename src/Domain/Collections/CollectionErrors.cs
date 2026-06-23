using SharedKernel;

namespace Domain.Collections;

public static class CollectionErrors
{
    public static readonly Error WatchAlreadyInCollection = Error.Conflict(
        "Collection.WatchAlreadyInCollection",
        "This watch is already in your collection");

    public static Error WatchNotFound(Guid watchId) => Error.NotFound(
        "Collection.WatchNotFound",
        $"Watch with id '{watchId}' was not found in the catalog");

    public static Error ItemNotFound(Guid watchId) => Error.NotFound(
        "Collection.ItemNotFound",
        $"Watch with id '{watchId}' was not found in your collection");
}
