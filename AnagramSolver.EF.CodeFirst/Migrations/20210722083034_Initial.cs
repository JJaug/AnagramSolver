using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CachedWords",
                columns: table => new
                {
                    Word = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Anagram = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWords", x => x.Word);
                });

            migrationBuilder.CreateTable(
                name: "SearchLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anagrams = table.Column<int>(type: "int", nullable: true),
                    SearchTime = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word1 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedWords");

            migrationBuilder.DropTable(
                name: "SearchLogs");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
