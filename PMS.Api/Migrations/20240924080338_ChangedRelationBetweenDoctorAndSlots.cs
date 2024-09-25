using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRelationBetweenDoctorAndSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctor_Slots_DoctorId",
                table: "Doctor_Slots");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Slots_DoctorId",
                table: "Doctor_Slots",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctor_Slots_DoctorId",
                table: "Doctor_Slots");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Slots_DoctorId",
                table: "Doctor_Slots",
                column: "DoctorId",
                unique: true);
        }
    }
}
