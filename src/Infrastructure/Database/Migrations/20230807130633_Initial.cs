using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arentheym.Database.Migrations
{
    /// <inheritdoc />
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Generated code")]
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApartmentConfigurations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DialToOpen = table.Column<bool>(type: "INTEGER", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    MemoryLocation = table.Column<short>(type: "INTEGER", nullable: false),
                    AccessCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intercoms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MasterCode = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intercoms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentConfigurationPhoneNumbers",
                columns: table => new
                {
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    ApartmentConfigurationId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentConfigurationPhoneNumbers", x => new { x.ApartmentConfigurationId, x.Order, x.Number });
                    table.ForeignKey(
                        name: "FK_ApartmentConfigurationPhoneNumbers_ApartmentConfigurations_ApartmentConfigurationId",
                        column: x => x.ApartmentConfigurationId,
                        principalTable: "ApartmentConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentConfigurationIntercoms",
                columns: table => new
                {
                    ApartmentConfigurationId = table.Column<string>(type: "TEXT", nullable: false),
                    IntercomsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentConfigurationIntercoms", x => new { x.ApartmentConfigurationId, x.IntercomsId });
                    table.ForeignKey(
                        name: "FK_ApartmentConfigurationIntercoms_ApartmentConfigurations_ApartmentConfigurationId",
                        column: x => x.ApartmentConfigurationId,
                        principalTable: "ApartmentConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentConfigurationIntercoms_Intercoms_IntercomsId",
                        column: x => x.IntercomsId,
                        principalTable: "Intercoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentConfigurationIntercoms_IntercomsId",
                table: "ApartmentConfigurationIntercoms",
                column: "IntercomsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ApartmentConfigurationIntercoms");
            migrationBuilder.DropTable(name: "ApartmentConfigurationPhoneNumbers");
            migrationBuilder.DropTable(name: "Intercoms");
            migrationBuilder.DropTable(name: "ApartmentConfigurations");
        }
    }
}