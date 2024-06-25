using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Configs;

public class SoftwareCategoryConfig : IEntityTypeConfiguration<SoftwareCategory>
{
    public void Configure(EntityTypeBuilder<SoftwareCategory> builder)
    {
        builder.HasKey(x => x.IdSoftwareCategory);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
    }
}