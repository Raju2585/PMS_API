using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class addingpropertiestomedicalhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasAsthma",
                table: "MedicalHistories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBloodPressure",
                table: "MedicalHistories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCancer",
                table: "MedicalHistories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCholesterol",
                table: "MedicalHistories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDiabetes",
                table: "MedicalHistories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHeartDisease",
                table: "MedicalHistories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Medication",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "HasAsthma",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "HasBloodPressure",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "HasCancer",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "HasCholesterol",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "HasDiabetes",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "HasHeartDisease",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "Medication",
                table: "MedicalHistories");
        }
    }
}
