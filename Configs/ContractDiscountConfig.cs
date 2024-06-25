using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Configs;

public class ContractDiscountConfig: IEntityTypeConfiguration<ContractDiscount>
{
    public void Configure(EntityTypeBuilder<ContractDiscount> builder)
    {
        builder.HasKey(x => new { x.IdDiscount, x.IdContract });
        builder
            .HasOne(x => x.Discount)
            .WithMany(x => x.ContractDiscounts)
            .HasForeignKey(x => x.IdDiscount)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(x => x.Contract)
            .WithMany(x => x.ContractDiscounts)
            .HasForeignKey(x => x.IdContract)
            .OnDelete(DeleteBehavior.NoAction);
    }
}