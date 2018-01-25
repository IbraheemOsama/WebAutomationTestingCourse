using Microsoft.EntityFrameworkCore.Migrations;

namespace Tax.Data.Migrations
{
    public partial class AddingUserTaxTableTaxDueAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TaxDueAmount",
                table: "UserTaxes",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxDueAmount",
                table: "UserTaxes");
        }
    }
}
