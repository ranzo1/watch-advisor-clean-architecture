using Domain.Watches.Entities;
using Domain.Watches.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class WatchConfiguration : IEntityTypeConfiguration<WatchAggregate>
{
    public void Configure(EntityTypeBuilder<WatchAggregate> builder)
    {
        builder.HasKey(w => w.Id);

        builder.ComplexProperty(w => w.Brand,
            b => b.Property(x => x.Value).HasColumnName("brand"));

        builder.ComplexProperty(w => w.Model,
            b => b.Property(x => x.Value).HasColumnName("model"));

        builder.ComplexProperty(w => w.ReferenceNumber,
            b => b.Property(x => x.Value).HasColumnName("reference_number"));

        builder.ComplexProperty(w => w.Dimensions, dimensions =>
        {
            dimensions.ComplexProperty(d => d.CaseDiameter,
                cd => cd.Property(c => c.Mm).HasColumnName("case_diameter_mm"));

            dimensions.Property(d => d.CaseThicknessMm).HasColumnName("case_thickness_mm");
            dimensions.Property(d => d.LugWidthMm).HasColumnName("lug_width_mm");
            dimensions.Property(d => d.LugToLugMm).HasColumnName("lug_to_lug_mm");
        });

        builder.ComplexProperty(w => w.Price,
            b => b.Property(x => x.Eur).HasColumnName("price_eur"));

        builder.ComplexProperty(w => w.DialColor,
            b => b.Property(x => x.Value).HasColumnName("dial_color"));

        builder.Property(w => w.ImageUrl)
            .HasConversion(u => u.ToString(), s => new Uri(s))
            .HasColumnName("image_url");

        builder.Property(w => w.Description)
            .HasConversion(
                d => d != null ? d.Value : null,
                s => s != null ? new Description(s) : null)
            .HasColumnName("description");
    }
}
