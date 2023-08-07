using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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

            migrationBuilder.InsertData(
                table: "ApartmentConfigurations",
                columns: new[] { "Id", "AccessCode", "DialToOpen", "DisplayName", "MemoryLocation" },
                values: new object[,]
                {
                    { "101", "", true, "101", (short)101 },
                    { "103", "", true, "103", (short)103 },
                    { "105", "", true, "105", (short)105 },
                    { "107", "", true, "107", (short)107 },
                    { "109", "", true, "109", (short)109 },
                    { "111", "", true, "111", (short)111 },
                    { "113", "", true, "113", (short)113 },
                    { "115", "", true, "115", (short)115 },
                    { "117", "", true, "117", (short)117 },
                    { "119", "", true, "119", (short)119 },
                    { "121", "", true, "121", (short)121 },
                    { "123", "", true, "123", (short)123 },
                    { "125", "", true, "125", (short)125 },
                    { "127", "", true, "127", (short)127 },
                    { "129", "", true, "129", (short)129 },
                    { "131", "", true, "131", (short)131 },
                    { "133", "", true, "133", (short)133 },
                    { "135", "", true, "135", (short)135 },
                    { "137", "", true, "137", (short)137 },
                    { "139", "", true, "139", (short)139 },
                    { "141", "", true, "141", (short)141 },
                    { "143", "", true, "143", (short)143 },
                    { "145", "", true, "145", (short)145 },
                    { "147", "", true, "147", (short)147 },
                    { "149", "", true, "149", (short)149 },
                    { "151", "", true, "151", (short)151 },
                    { "153", "", true, "153", (short)153 },
                    { "155", "", true, "155", (short)155 },
                    { "157", "", true, "157", (short)157 },
                    { "159", "", true, "159", (short)159 },
                    { "161", "", true, "161", (short)161 },
                    { "163", "", true, "163", (short)163 },
                    { "165", "", true, "165", (short)165 },
                    { "167", "", true, "167", (short)167 },
                    { "169", "", true, "169", (short)169 },
                    { "171", "", true, "171", (short)171 },
                    { "173", "", true, "173", (short)173 },
                    { "175", "", true, "175", (short)175 },
                    { "177", "", true, "177", (short)177 },
                    { "179", "", true, "179", (short)179 },
                    { "181", "", true, "181", (short)181 },
                    { "183", "", true, "183", (short)183 },
                    { "185", "", true, "185", (short)185 },
                    { "187", "", true, "187", (short)187 },
                    { "189", "", true, "189", (short)189 },
                    { "51", "", true, "051", (short)51 },
                    { "53", "", true, "053", (short)53 },
                    { "55", "", true, "055", (short)55 },
                    { "57", "", true, "057", (short)57 },
                    { "59", "", true, "059", (short)59 },
                    { "61", "", true, "061", (short)61 },
                    { "63", "", true, "063", (short)63 },
                    { "65", "", true, "065", (short)65 },
                    { "67", "", true, "067", (short)67 },
                    { "69", "", true, "069", (short)69 },
                    { "71", "", true, "071", (short)71 },
                    { "73", "", true, "073", (short)73 },
                    { "75", "", true, "075", (short)75 },
                    { "77", "", true, "077", (short)77 },
                    { "79", "", true, "079", (short)79 },
                    { "81", "", true, "081", (short)81 },
                    { "83", "", true, "083", (short)83 },
                    { "85", "", true, "085", (short)85 },
                    { "87", "", true, "087", (short)87 },
                    { "89", "", true, "089", (short)89 },
                    { "91", "", true, "091", (short)91 },
                    { "93", "", true, "093", (short)93 },
                    { "95", "", true, "095", (short)95 },
                    { "97", "", true, "097", (short)97 },
                    { "99", "", true, "099", (short)99 }
                });

            migrationBuilder.InsertData(
                table: "Intercoms",
                columns: new[] { "Id", "MasterCode", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("0969697d-3314-4f9e-ac6f-007ccfed6e9e"), "8601", "Slagboom achter", "0657181402" },
                    { new Guid("6d671f75-9f3d-4f3c-8eb3-7d5200579ef8"), "8601", "Slagboom voor", "0657093298" }
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