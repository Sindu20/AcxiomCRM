
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcxiomCRM.Migrations
{
    public partial class AddLeadTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    LeadId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),

                    LeadName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),

                    Email = table.Column<string>(type: "TEXT", nullable: false),

                    Phone = table.Column<string>(type: "TEXT", nullable: false),

                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),

                    Source = table.Column<string>(type: "TEXT", nullable: false),

                    Status = table.Column<string>(type: "TEXT", nullable: false),

                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.LeadId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leads");
        }
    }
}
