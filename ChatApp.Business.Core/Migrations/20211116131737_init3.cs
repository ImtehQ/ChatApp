using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Business.Core.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresVerification",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequiresVerification",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
