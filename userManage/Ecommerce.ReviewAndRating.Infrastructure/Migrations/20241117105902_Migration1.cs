using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.ReviewAndRating.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelSearch",
                columns: table => new
                {
                    feedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feedbackMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rate = table.Column<int>(type: "int", nullable: false),
                    givenDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelSearch", x => x.feedbackId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelSearch");
        }
    }
}
