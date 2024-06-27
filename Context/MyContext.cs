using Microsoft.EntityFrameworkCore;
using Project.Configs;
using Project.Entities;
using Version = Project.Entities.Version;

namespace ApbdProject.Context;

public class MyContext : DbContext
{
    public MyContext()
    {
    }

    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    } 
    public DbSet<Company> Companies { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<SoftwareCategory> SoftwareCategories { get; set; }
    public DbSet<Version> Versions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CompanyConfig());
        modelBuilder.ApplyConfiguration(new ContractConfig());
        modelBuilder.ApplyConfiguration(new DiscountConfig());
        modelBuilder.ApplyConfiguration(new IndividualConfig());
        modelBuilder.ApplyConfiguration(new PaymentConfig());
        modelBuilder.ApplyConfiguration(new SoftwareConfig());
        modelBuilder.ApplyConfiguration(new SoftwareCategoryConfig());
        modelBuilder.ApplyConfiguration(new VersionConfig());

        modelBuilder.Entity<Contract>()
            .Property(x => x.Status).IsConcurrencyToken();
        modelBuilder.Entity<Contract>()
            .Property(x => x.AmountPaid).IsConcurrencyToken();

    }
}
