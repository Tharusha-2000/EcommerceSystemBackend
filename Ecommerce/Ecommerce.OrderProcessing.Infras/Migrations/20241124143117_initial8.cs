using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.OrderProcessing.Infras.Migrations
{
    /// <inheritdoc />
    public partial class initial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "productImg",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "productName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "unitPrice",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productImg",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "productName",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "unitPrice",
                table: "Carts");
        }
    }
}
