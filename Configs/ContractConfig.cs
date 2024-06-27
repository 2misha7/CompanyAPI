using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Configs;

public class ContractConfig : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(x => x.IdContract);
        builder.Property(x => x.DateFrom).IsRequired();
        builder.Property(x => x.DateTo).IsRequired();
        builder.Property(x => x.ExtendedSupportPeriod).IsRequired();
        builder.Property(x=>x.Status).IsRequired();
        builder
            .HasOne(x => x.Company)
            .WithMany(x => x.Contracts)
            .HasForeignKey(x => x.IdCompany)
            .IsRequired(false);
        builder
            .HasOne(x => x.Individual)
            .WithMany(x => x.Contracts)
            .HasForeignKey(x => x.IdIndividual)
            .IsRequired(false);
        builder
            .HasOne(x => x.Version)
            .WithMany(x => x.Contracts)
            .HasForeignKey(x => x.IdSoftwareVersion)
            .IsRequired();

    }
}