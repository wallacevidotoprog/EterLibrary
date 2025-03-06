using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EterLibrary.Migrations
{
    /// <inheritdoc />
    public partial class INIT_C : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedManipulation_Manipulation_ManipulationID",
                table: "MedManipulation");

            migrationBuilder.DropIndex(
                name: "IX_MedManipulation_ManipulationID",
                table: "MedManipulation");

            migrationBuilder.DropColumn(
                name: "ManipulationID",
                table: "MedManipulation");

            migrationBuilder.CreateIndex(
                name: "IX_MedManipulation_ID_MANIPULADOS",
                table: "MedManipulation",
                column: "ID_MANIPULADOS");

            migrationBuilder.AddForeignKey(
                name: "FK_MedManipulation_Manipulation_ID_MANIPULADOS",
                table: "MedManipulation",
                column: "ID_MANIPULADOS",
                principalTable: "Manipulation",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedManipulation_Manipulation_ID_MANIPULADOS",
                table: "MedManipulation");

            migrationBuilder.DropIndex(
                name: "IX_MedManipulation_ID_MANIPULADOS",
                table: "MedManipulation");

            migrationBuilder.AddColumn<long>(
                name: "ManipulationID",
                table: "MedManipulation",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedManipulation_ManipulationID",
                table: "MedManipulation",
                column: "ManipulationID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedManipulation_Manipulation_ManipulationID",
                table: "MedManipulation",
                column: "ManipulationID",
                principalTable: "Manipulation",
                principalColumn: "ID");
        }
    }
}
