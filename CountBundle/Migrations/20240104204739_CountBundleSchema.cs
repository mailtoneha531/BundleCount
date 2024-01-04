using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountBundle.Migrations
{
    /// <inheritdoc />
    public partial class CountBundleSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    BundleEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPairExist = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.BundleEntityId);
                });

            migrationBuilder.CreateTable(
                name: "BundleParts",
                columns: table => new
                {
                    BundlePartEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPairExist = table.Column<bool>(type: "bit", nullable: false),
                    InventoryCount = table.Column<int>(type: "int", nullable: false),
                    BundleEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleParts", x => x.BundlePartEntityId);
                    table.ForeignKey(
                        name: "FK_BundleParts_Bundles_BundleEntityId",
                        column: x => x.BundleEntityId,
                        principalTable: "Bundles",
                        principalColumn: "BundleEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BundlePartSubEntity",
                columns: table => new
                {
                    BundleSubEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPairExist = table.Column<bool>(type: "bit", nullable: false),
                    InventoryCount = table.Column<int>(type: "int", nullable: false),
                    BundlePartEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundlePartSubEntity", x => x.BundleSubEntityId);
                    table.ForeignKey(
                        name: "FK_BundlePartSubEntity_BundleParts_BundlePartEntityId",
                        column: x => x.BundlePartEntityId,
                        principalTable: "BundleParts",
                        principalColumn: "BundlePartEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BundleParts_BundleEntityId",
                table: "BundleParts",
                column: "BundleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BundlePartSubEntity_BundlePartEntityId",
                table: "BundlePartSubEntity",
                column: "BundlePartEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BundlePartSubEntity");

            migrationBuilder.DropTable(
                name: "BundleParts");

            migrationBuilder.DropTable(
                name: "Bundles");
        }
    }
}
