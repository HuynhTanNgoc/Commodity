using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Commodity.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsImportOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportOrder",
                columns: table => new
                {
                    ImportOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportOrderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportOrder", x => x.ImportOrderID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportOrder");
        }
    }
}
