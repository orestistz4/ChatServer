using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatServer.Migrations
{
    public partial class ConfigureGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Groups",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Groups",
                newName: "Name");
        }
    }
}
