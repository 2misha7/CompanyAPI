using GakkoHorizontalSlice.Model;
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
    public DbSet<AppUser> Users { get; set; }
    
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
        
        
        
        modelBuilder.Entity<Company>().HasData(
            new Company { IdCompany = 1, CompanyName = "Company A", Address = "123 Main St", Email = "companyA@example.com", PhoneNumber = "123-456-7890", KRS = "1234567890" },
            new Company { IdCompany = 2, CompanyName = "Company B", Address = "456 Elm St", Email = "companyB@example.com", PhoneNumber = "987-654-3210", KRS = "9876543210" }
        );
        
        modelBuilder.Entity<Individual>().HasData(
            new Individual { IdIndividual = 1, FirstName = "John", LastName = "Doe", Address = "789 Oak St", Email = "john.doe@example.com", PhoneNumber = "456-789-0123", PESEL = "12345678901" },
            new Individual { IdIndividual = 2, FirstName = "Jane", LastName = "Smith", Address = "987 Pine St", Email = "jane.smith@example.com", PhoneNumber = "789-012-3456", PESEL = "98765432109" }
        );

        modelBuilder.Entity<SoftwareCategory>().HasData(
           new SoftwareCategory {IdSoftwareCategory = 1, Name = "Education"},
           new SoftwareCategory {IdSoftwareCategory = 2, Name = "Accounting"}
        );
        modelBuilder.Entity<Version>().HasData(
            new Version { IdVersion = 1, IdSoftware = 1, Date = new DateOnly(2023, 1, 1), Description = "Version 1.0", Name = "V1", Price = 1999.99 },
            new Version { IdVersion = 2, IdSoftware = 1, Date = new DateOnly(2024, 6, 1), Description = "Version 2.0", Name = "V2", Price = 2999.99 },
            new Version { IdVersion = 3, IdSoftware = 2, Date = new DateOnly(2023, 1, 1), Description = "Version 1.0", Name = "V1", Price = 1499.99 },
            new Version { IdVersion = 4, IdSoftware = 2, Date = new DateOnly(2024, 6, 1), Description = "Version 2.0", Name = "V2", Price = 2499.99 }
        );

        modelBuilder.Entity<Software>().HasData(
            new Software { IdSoftware = 1, Name = "Software A", Description = "Education software A", IdSoftwareCategory = 1 },
            new Software { IdSoftware = 2, Name = "Software B", Description = "Accounting software B", IdSoftwareCategory = 2 }
        );
        
        modelBuilder.Entity<Discount>().HasData(
            new Discount { IdDiscount = 1, Name = "Discount", Percentage = 10.0, DateFrom = DateTime.Parse("2023-01-01 00:00:00"), DateTo = DateTime.Parse("2025-12-31 00:00:00") },
            new Discount { IdDiscount = 2, Name = "Summer Sale", Percentage = 15.0, DateFrom = DateTime.Parse("2023-06-01 00:00:00"), DateTo = DateTime.Parse("2023-08-31 00:00:00") }
        );
        
        
        modelBuilder.Entity<Contract>().HasData(
            new Contract
            {
                IdContract = 1,
                DateFrom = DateTime.Parse("2024-06-27 00:00:00"),
                IdIndividual = 1, 
                IdCompany = null, 
                DateTo = DateTime.Parse("2024-07-20 00:00:00"),
                IdSoftwareVersion = 1, 
                FullPrice = 1000.0,
                Status = ContractStatuses.Created,
                ExtendedSupportPeriod = 1,
                AmountPaid = 500.0
            },
            new Contract
            {
                IdContract = 2,
                DateFrom = DateTime.Parse("2023-02-01 00:00:00"),
                IdIndividual = null, 
                IdCompany = 1,
                DateTo = DateTime.Parse("2023-02-27 00:00:00"),
                IdSoftwareVersion = 2, 
                FullPrice = 1500.0,
                Status = ContractStatuses.Signed,
                ExtendedSupportPeriod = 2,
                AmountPaid = 1500.0
            }
           
        );
    }
}
