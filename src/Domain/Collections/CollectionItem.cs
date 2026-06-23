using SharedKernel;

namespace Domain.Collections;

public sealed class CollectionItem : Entity
{
    private CollectionItem(Guid id, Guid userId, Guid watchId, string? notes, DateTime addedAt)
        : base(id)
    {
        UserId = userId;
        WatchId = watchId;
        Notes = notes;
        AddedAt = addedAt;
    }

    private CollectionItem()
    {
    }

    public Guid UserId { get; private set; }
    public Guid WatchId { get; private set; }
    public string? Notes { get; private set; }
    public DateTime AddedAt { get; private set; }

    public static CollectionItem Create(Guid userId, Guid watchId, string? notes) =>
        new(Guid.NewGuid(), userId, watchId, notes, DateTime.UtcNow);

    public void UpdateNotes(string? notes) => Notes = notes;
}
