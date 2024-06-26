using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Configs;

public class VersionConfig : IEntityTypeConfiguration<Entities.Version>
{
    public void Configure(EntityTypeBuilder<Entities.Version> builder)
    {
        builder.HasKey(x => x.IdVersion);
        builder
            .HasOne(x => x.Software)
            .WithMany(x => x.Versions)
            .HasForeignKey(x => x.IdSoftware)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
        builder.Property(x => x.Price).IsRequired();
    }
}