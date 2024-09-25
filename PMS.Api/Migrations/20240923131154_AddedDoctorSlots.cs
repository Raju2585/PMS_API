using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedDoctorSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor_Slots",
                columns: table => new
                {
                    SlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    Slot_1 = table.Column<bool>(type: "bit", nullable: false),
                    Slot_2 = table.Column<bool>(type: "bit", nullable: false),
                    Slot_3 = table.Column<bool>(type: "bit", nullable: false),
                    Slot_4 = table.Column<bool>(type: "bit", nullable: false),
                    Slot_5 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_Slots", x => x.SlotId);
                    table.ForeignKey(
                        name: "FK_Doctor_Slots_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Slots_DoctorId",
                table: "Doctor_Slots",
                column: "DoctorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor_Slots");
        }
    }
}
