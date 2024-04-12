using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IChibanGameServer.Migrations
{
    /// <inheritdoc />
    public partial class addLot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LotBox",
                table: "LotBox");

            migrationBuilder.RenameTable(
                name: "LotBox",
                newName: "LotBoxs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LotBoxs",
                table: "LotBoxs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    BelongBoxId = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: false),
                    FinishedDateTime = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LotBoxs",
                table: "LotBoxs");

            migrationBuilder.RenameTable(
                name: "LotBoxs",
                newName: "LotBox");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LotBox",
                table: "LotBox",
                column: "Id");
        }
    }
}
