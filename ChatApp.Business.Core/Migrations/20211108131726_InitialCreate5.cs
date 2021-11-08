using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Business.Core.Migrations
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    isBlocked = table.Column<bool>(type: "bit", nullable: false),
                    RequiresVerification = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlobContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    type = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "Email", "FirstName", "LastName", "LastUpdated", "PasswordHash", "RequiresVerification", "RoleId", "UserName", "isBlocked" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 8, 14, 17, 25, 716, DateTimeKind.Local).AddTicks(7275), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 720, DateTimeKind.Local).AddTicks(4796), "", false, 0, "UserName", false },
                    { 2, new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9676), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9696), "", false, 0, "UserName", false },
                    { 3, new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9801), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9805), "", false, 0, "UserName", false },
                    { 4, new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9825), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9828), "", false, 0, "UserName", false },
                    { 5, new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9844), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9847), "", false, 0, "UserName", false },
                    { 6, new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9871), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9874), "", false, 0, "UserName", false },
                    { 7, new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9890), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9893), "", false, 0, "UserName", false },
                    { 8, new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9909), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9912), "", false, 0, "UserName", false },
                    { 9, new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9929), "Email@Email.com", "FirstName", "LastName", new DateTime(2021, 11, 8, 14, 17, 25, 721, DateTimeKind.Local).AddTicks(9931), "", false, 0, "UserName", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
