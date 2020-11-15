using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppNumberSorter.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SortDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnsortedNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortedNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortTime = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SortDetails");
        }
    }
}
