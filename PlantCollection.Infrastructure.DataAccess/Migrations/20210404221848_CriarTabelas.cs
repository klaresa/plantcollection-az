using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantCollection.Infrastructure.DataAccess.Migrations
{
    public partial class CriarTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BinomialName = table.Column<string>(nullable: true),
                    Species = table.Column<string>(nullable: true),
                    Genus = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true),
                    PageViews = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plant");
        }
    }
}
