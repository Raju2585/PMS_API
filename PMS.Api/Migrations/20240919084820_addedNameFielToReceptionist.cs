using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedNameFielToReceptionist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceptionistName",
                table: "Receptionists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceptionistName",
                table: "Receptionists");
        }
    }
}
