using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApbdProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    IdCompany = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KRS = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.IdCompany);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    IdDiscount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.IdDiscount);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    IdIndividual = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PESEL = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.IdIndividual);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareCategories",
                columns: table => new
                {
                    IdSoftwareCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareCategories", x => x.IdSoftwareCategory);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    IdSoftwareCategory = table.Column<int>(type: "int", nullable: false),
                    IdSoftware = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.IdSoftwareCategory);
                    table.ForeignKey(
                        name: "FK_Softwares_SoftwareCategories_IdSoftwareCategory",
                        column: x => x.IdSoftwareCategory,
                        principalTable: "SoftwareCategories",
                        principalColumn: "IdSoftwareCategory");
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    IdVersion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSoftware = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.IdVersion);
                    table.ForeignKey(
                        name: "FK_Versions_Softwares_IdSoftware",
                        column: x => x.IdSoftware,
                        principalTable: "Softwares",
                        principalColumn: "IdSoftwareCategory");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    IdContract = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdIndividual = table.Column<int>(type: "int", nullable: false),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdSoftwareVersion = table.Column<int>(type: "int", nullable: false),
                    FullPrice = table.Column<double>(type: "float", nullable: false),
                    ExtendedSupportPeriod = table.Column<int>(type: "int", nullable: false),
                    AmountPaid = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.IdContract);
                    table.ForeignKey(
                        name: "FK_Contracts_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany");
                    table.ForeignKey(
                        name: "FK_Contracts_Individuals_IdIndividual",
                        column: x => x.IdIndividual,
                        principalTable: "Individuals",
                        principalColumn: "IdIndividual");
                    table.ForeignKey(
                        name: "FK_Contracts_Versions_IdSoftwareVersion",
                        column: x => x.IdSoftwareVersion,
                        principalTable: "Versions",
                        principalColumn: "IdVersion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractDiscounts",
                columns: table => new
                {
                    IdContract = table.Column<int>(type: "int", nullable: false),
                    IdDiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDiscounts", x => new { x.IdDiscount, x.IdContract });
                    table.ForeignKey(
                        name: "FK_ContractDiscounts_Contracts_IdContract",
                        column: x => x.IdContract,
                        principalTable: "Contracts",
                        principalColumn: "IdContract");
                    table.ForeignKey(
                        name: "FK_ContractDiscounts_Discounts_IdDiscount",
                        column: x => x.IdDiscount,
                        principalTable: "Discounts",
                        principalColumn: "IdDiscount");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdContract = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.IdPayment);
                    table.ForeignKey(
                        name: "FK_Payments_Contracts_IdContract",
                        column: x => x.IdContract,
                        principalTable: "Contracts",
                        principalColumn: "IdContract");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Email",
                table: "Companies",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_KRS",
                table: "Companies",
                column: "KRS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PhoneNumber",
                table: "Companies",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractDiscounts_IdContract",
                table: "ContractDiscounts",
                column: "IdContract");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdCompany",
                table: "Contracts",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdIndividual",
                table: "Contracts",
                column: "IdIndividual");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdSoftwareVersion",
                table: "Contracts",
                column: "IdSoftwareVersion");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_Email",
                table: "Individuals",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_PESEL",
                table: "Individuals",
                column: "PESEL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_PhoneNumber",
                table: "Individuals",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdContract",
                table: "Payments",
                column: "IdContract");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_IdSoftware",
                table: "Versions",
                column: "IdSoftware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDiscounts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "SoftwareCategories");
        }
    }
}
