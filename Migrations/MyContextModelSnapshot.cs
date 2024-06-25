﻿// <auto-generated />
using System;
using ApbdProject.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApbdProject.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project.Entities.Company", b =>
                {
                    b.Property<int>("IdCompany")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCompany"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdCompany");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("KRS")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Project.Entities.Contract", b =>
                {
                    b.Property<int>("IdContract")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContract"));

                    b.Property<double>("AmountPaid")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExtendedSupportPeriod")
                        .HasColumnType("int");

                    b.Property<double>("FullPrice")
                        .HasColumnType("float");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int");

                    b.Property<int>("IdIndividual")
                        .HasColumnType("int");

                    b.Property<int>("IdSoftwareVersion")
                        .HasColumnType("int");

                    b.HasKey("IdContract");

                    b.HasIndex("IdCompany");

                    b.HasIndex("IdIndividual");

                    b.HasIndex("IdSoftwareVersion");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Project.Entities.ContractDiscount", b =>
                {
                    b.Property<int>("IdDiscount")
                        .HasColumnType("int");

                    b.Property<int>("IdContract")
                        .HasColumnType("int");

                    b.HasKey("IdDiscount", "IdContract");

                    b.HasIndex("IdContract");

                    b.ToTable("ContractDiscounts");
                });

            modelBuilder.Entity("Project.Entities.Discount", b =>
                {
                    b.Property<int>("IdDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDiscount"));

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Percentage")
                        .HasColumnType("float");

                    b.HasKey("IdDiscount");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Project.Entities.Individual", b =>
                {
                    b.Property<int>("IdIndividual")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdIndividual"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .IsFixedLength();

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdIndividual");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PESEL")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Individuals");
                });

            modelBuilder.Entity("Project.Entities.Payment", b =>
                {
                    b.Property<int>("IdPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPayment"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdContract")
                        .HasColumnType("int");

                    b.HasKey("IdPayment");

                    b.HasIndex("IdContract");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Project.Entities.Software", b =>
                {
                    b.Property<int>("IdSoftwareCategory")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdSoftwareCategory");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("Project.Entities.SoftwareCategory", b =>
                {
                    b.Property<int>("IdSoftwareCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSoftwareCategory"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdSoftwareCategory");

                    b.ToTable("SoftwareCategories");
                });

            modelBuilder.Entity("Project.Entities.Version", b =>
                {
                    b.Property<int>("IdVersion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVersion"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int");

                    b.HasKey("IdVersion");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("Project.Entities.Contract", b =>
                {
                    b.HasOne("Project.Entities.Company", "Company")
                        .WithMany("Contracts")
                        .HasForeignKey("IdCompany");

                    b.HasOne("Project.Entities.Individual", "Individual")
                        .WithMany("Contracts")
                        .HasForeignKey("IdIndividual");

                    b.HasOne("Project.Entities.Version", "Version")
                        .WithMany("Contracts")
                        .HasForeignKey("IdSoftwareVersion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Individual");

                    b.Navigation("Version");
                });

            modelBuilder.Entity("Project.Entities.ContractDiscount", b =>
                {
                    b.HasOne("Project.Entities.Contract", "Contract")
                        .WithMany("ContractDiscounts")
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Project.Entities.Discount", "Discount")
                        .WithMany("ContractDiscounts")
                        .HasForeignKey("IdDiscount")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("Project.Entities.Payment", b =>
                {
                    b.HasOne("Project.Entities.Contract", "Contract")
                        .WithMany("Payments")
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("Project.Entities.Software", b =>
                {
                    b.HasOne("Project.Entities.SoftwareCategory", "SoftwareCategory")
                        .WithMany("Softwares")
                        .HasForeignKey("IdSoftwareCategory")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SoftwareCategory");
                });

            modelBuilder.Entity("Project.Entities.Version", b =>
                {
                    b.HasOne("Project.Entities.Software", "Software")
                        .WithMany("Versions")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Software");
                });

            modelBuilder.Entity("Project.Entities.Company", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("Project.Entities.Contract", b =>
                {
                    b.Navigation("ContractDiscounts");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Project.Entities.Discount", b =>
                {
                    b.Navigation("ContractDiscounts");
                });

            modelBuilder.Entity("Project.Entities.Individual", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("Project.Entities.Software", b =>
                {
                    b.Navigation("Versions");
                });

            modelBuilder.Entity("Project.Entities.SoftwareCategory", b =>
                {
                    b.Navigation("Softwares");
                });

            modelBuilder.Entity("Project.Entities.Version", b =>
                {
                    b.Navigation("Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}
