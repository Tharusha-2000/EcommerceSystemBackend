using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.OrderProcessing.Infras.Migrations
{
    /// <inheritdoc />
    public partial class initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "state",
                table: "Orders",
                newName: "province");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "province",
                table: "Orders",
                newName: "state");
        }
    }
}
