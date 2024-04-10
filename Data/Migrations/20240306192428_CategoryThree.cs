using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryThree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryThree",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryTwoid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryThree", x => x.id);
                    table.ForeignKey(
                        name: "FK_CategoryThree_CategoryTwo_CategoryTwoid",
                        column: x => x.CategoryTwoid,
                        principalTable: "CategoryTwo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryThree_CategoryTwoid",
                table: "CategoryThree",
                column: "CategoryTwoid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryThree");
        }
    }
}
