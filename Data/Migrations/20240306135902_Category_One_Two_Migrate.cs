using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Category_One_Two_Migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryOne",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryOne", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTwo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryOneid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTwo", x => x.id);
                    table.ForeignKey(
                        name: "FK_CategoryTwo_CategoryOne_CategoryOneid",
                        column: x => x.CategoryOneid,
                        principalTable: "CategoryOne",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTwo_CategoryOneid",
                table: "CategoryTwo",
                column: "CategoryOneid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTwo");

            migrationBuilder.DropTable(
                name: "CategoryOne");
        }
    }
}
