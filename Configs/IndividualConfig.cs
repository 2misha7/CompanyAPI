using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Configs;

public class IndividualConfig : IEntityTypeConfiguration<Individual>
{
    public void Configure(EntityTypeBuilder<Individual> builder)
    {
        builder.HasKey(x => x.IdIndividual);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(e => e.PESEL)
            .IsRequired()
            .HasMaxLength(11) 
            .IsFixedLength();
        
        builder.HasIndex(x => x.PESEL)
            .IsUnique();
        
        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
}