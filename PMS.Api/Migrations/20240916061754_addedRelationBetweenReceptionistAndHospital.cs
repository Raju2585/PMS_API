using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedRelationBetweenReceptionistAndHospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Receptionists");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Receptionists",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Receptionists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_HospitalId",
                table: "Receptionists",
                column: "HospitalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receptionists_Hospitals_HospitalId",
                table: "Receptionists",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "HospitalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptionists_Hospitals_HospitalId",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Receptionists_HospitalId",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Receptionists");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Receptionists",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Receptionists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
