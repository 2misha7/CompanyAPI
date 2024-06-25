using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Configs;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.IdPayment);
        builder
            .HasOne(x => x.Contract)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.IdContract)
            .OnDelete(DeleteBehavior.NoAction);
    }
}