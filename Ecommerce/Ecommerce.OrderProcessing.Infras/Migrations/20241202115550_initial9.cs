using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.OrderProcessing.Infras.Migrations
{
    /// <inheritdoc />
    public partial class initial9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "paymentMethod",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Orders",
                newName: "lName");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "Orders",
                newName: "fName");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "Orders",
                newName: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lName",
                table: "Orders",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "fName",
                table: "Orders",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Orders",
                newName: "province");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "paymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
