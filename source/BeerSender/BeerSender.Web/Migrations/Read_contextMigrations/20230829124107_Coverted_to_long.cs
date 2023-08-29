using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSender.Web.Migrations.Read_contextMigrations
{
    /// <inheritdoc />
    public partial class Coverted_to_long : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Checkpoint",
                table: "Checkpoints",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Checkpoint",
                table: "Checkpoints",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
