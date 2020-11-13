using Microsoft.EntityFrameworkCore.Migrations;

namespace Descartes.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DifferenceObject",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    DiffResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DifferenceObject", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DifferenceObject");
        }
    }
}
