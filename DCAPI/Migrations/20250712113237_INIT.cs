using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DCAPI.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clinicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dentists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    ClinicalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dentists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dentists_Clinicals_ClinicalId",
                        column: x => x.ClinicalId,
                        principalTable: "Clinicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClinicalId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalCustomer",
                columns: table => new
                {
                    ClinicalsId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalCustomer", x => new { x.ClinicalsId, x.CustomersId });
                    table.ForeignKey(
                        name: "FK_ClinicalCustomer_Clinicals_ClinicalsId",
                        column: x => x.ClinicalsId,
                        principalTable: "Clinicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicalCustomer_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clinicals",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Klinik 1" },
                    { 2, "Klinik 2" },
                    { 3, "Klinik 3" },
                    { 4, "Klinik A1" },
                    { 5, "Klinik A2" },
                    { 6, "Klinik B1" },
                    { 7, "Klinik B2" },
                    { 8, "Klinik C1" },
                    { 9, "Klinik C2" }
                });

            migrationBuilder.InsertData(
                table: "Dentists",
                columns: new[] { "Id", "ClinicalId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 3, "Umut Berk", "Demir" },
                    { 2, 1, "Elif Naz", "Yıldız" },
                    { 3, 5, "Mert Can", "Koç" },
                    { 4, 2, "Zeynep", "Aydın" },
                    { 5, 4, "Ali Eren", "Şahin" },
                    { 6, 1, "Ayşe", "Kara" },
                    { 7, 5, "Emirhan", "Yılmaz" },
                    { 8, 3, "Melisa", "Çelik" },
                    { 9, 2, "Berkay", "Arslan" },
                    { 10, 4, "İlayda", "Öztürk" },
                    { 11, 2, "Deniz", "Aksoy" },
                    { 12, 1, "Kaan", "Bozkurt" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalCustomer_CustomersId",
                table: "ClinicalCustomer",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Dentists_ClinicalId",
                table: "Dentists",
                column: "ClinicalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "ClinicalCustomer");

            migrationBuilder.DropTable(
                name: "Dentists");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Clinicals");
        }
    }
}
