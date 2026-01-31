using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnTamNghe.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToPriorityContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "PriorityContacts");

            migrationBuilder.RenameColumn(
                name: "PriorityContactId",
                table: "PriorityContacts",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "PriorityLevel",
                table: "PriorityContacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "PriorityContacts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PriorityContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PriorityContacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PriorityContacts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PriorityContacts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PriorityContacts",
                newName: "PriorityContactId");

            migrationBuilder.AlterColumn<string>(
                name: "PriorityLevel",
                table: "PriorityContacts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "PriorityContacts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "PriorityContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
