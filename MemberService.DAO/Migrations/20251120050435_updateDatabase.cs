using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberService.DAO.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transaction_code",
                table: "order");

            migrationBuilder.AddColumn<string>(
                name: "transaction_code",
                table: "payment",
                type: "varchar(150)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transaction_code",
                table: "payment");

            migrationBuilder.AddColumn<string>(
                name: "transaction_code",
                table: "order",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
