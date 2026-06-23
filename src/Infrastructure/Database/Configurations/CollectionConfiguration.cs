using Domain.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class CollectionConfiguration : IEntityTypeConfiguration<CollectionItem>
{
    public void Configure(EntityTypeBuilder<CollectionItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.ToTable("user_collections");

        builder.Property(c => c.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(c => c.WatchId).HasColumnName("watch_id").IsRequired();
        builder.Property(c => c.Notes).HasColumnName("notes");
        builder.Property(c => c.AddedAt).HasColumnName("added_at").IsRequired();

        builder.HasIndex(c => new { c.UserId, c.WatchId }).IsUnique();
    }
}
