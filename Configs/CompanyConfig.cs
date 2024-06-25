using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Configs;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.IdCompany);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.KRS).IsRequired().HasMaxLength(14);
        
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.HasIndex(x => x.KRS)
            .IsUnique();
        
        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();
    }
}