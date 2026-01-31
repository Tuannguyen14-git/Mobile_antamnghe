using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnTamNghe.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToCallLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CallTime",
                table: "CallLogs",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CallLogId",
                table: "CallLogs",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "CallLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "CallLogs",
                newName: "CallTime");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CallLogs",
                newName: "CallLogId");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "CallLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
