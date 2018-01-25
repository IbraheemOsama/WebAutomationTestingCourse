using Microsoft.EntityFrameworkCore.Migrations;

namespace Tax.Data.Migrations
{
    public partial class AddingUserTaxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTaxes",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CharityPaidAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    NumberOfChildren = table.Column<short>(type: "smallint", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaxes", x => new { x.Year, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTaxes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTaxes_UserId",
                table: "UserTaxes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTaxes");
        }
    }
}
