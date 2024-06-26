using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Configs;

public class SoftwareConfig : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        builder.HasKey(x => x.IdSoftware);
        builder
            .HasOne(x => x.SoftwareCategory)
            .WithMany(x => x.Softwares)
            .HasForeignKey(x => x.IdSoftwareCategory)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(150);
    }
}