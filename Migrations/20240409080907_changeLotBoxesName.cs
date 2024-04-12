using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IChibanGameServer.Migrations
{
    /// <inheritdoc />
    public partial class changeLotBoxesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LotBoxs",
                table: "LotBoxs");

            migrationBuilder.RenameTable(
                name: "LotBoxs",
                newName: "LotBoxes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LotBoxes",
                table: "LotBoxes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LotBoxes",
                table: "LotBoxes");

            migrationBuilder.RenameTable(
                name: "LotBoxes",
                newName: "LotBoxs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LotBoxs",
                table: "LotBoxs",
                column: "Id");
        }
    }
}
