using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspProjektPojisteni.Data.Migrations
{
    public partial class VytvoreniDatabaze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Policyholder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policyholder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceRate = table.Column<int>(type: "int", nullable: false),
                    InsuranceStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsuranceEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PolicyholderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Insurance_Policyholder_PolicyholderID",
                        column: x => x.PolicyholderID,
                        principalTable: "Policyholder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceEvent",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payout = table.Column<int>(type: "int", nullable: false),
                    PolicyholderID = table.Column<int>(type: "int", nullable: false),
                    InsuranceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceEvent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InsuranceEvent_Insurance_InsuranceID",
                        column: x => x.InsuranceID,
                        principalTable: "Insurance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceEvent_Policyholder_PolicyholderID",
                        column: x => x.PolicyholderID,
                        principalTable: "Policyholder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_PolicyholderID",
                table: "Insurance",
                column: "PolicyholderID");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvent_InsuranceID",
                table: "InsuranceEvent",
                column: "InsuranceID");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvent_PolicyholderID",
                table: "InsuranceEvent",
                column: "PolicyholderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceEvent");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropTable(
                name: "Policyholder");
        }
    }
}
