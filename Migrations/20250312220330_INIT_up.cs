using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EterLibrary.Migrations
{
    /// <inheritdoc />
    public partial class INIT_up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlReqNota_UserPossition_AUTHOR",
                table: "ControlReqNota");

            migrationBuilder.DropForeignKey(
                name: "FK_MedManipulation_Manipulation_ID_MANIPULADOS",
                table: "MedManipulation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductValidade_Vality_ID_VALIDADE",
                table: "ProductValidade");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlReqNota_UserPossition_AUTHOR",
                table: "ControlReqNota",
                column: "AUTHOR",
                principalTable: "UserPossition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedManipulation_Manipulation_ID_MANIPULADOS",
                table: "MedManipulation",
                column: "ID_MANIPULADOS",
                principalTable: "Manipulation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductValidade_Vality_ID_VALIDADE",
                table: "ProductValidade",
                column: "ID_VALIDADE",
                principalTable: "Vality",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlReqNota_UserPossition_AUTHOR",
                table: "ControlReqNota");

            migrationBuilder.DropForeignKey(
                name: "FK_MedManipulation_Manipulation_ID_MANIPULADOS",
                table: "MedManipulation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductValidade_Vality_ID_VALIDADE",
                table: "ProductValidade");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlReqNota_UserPossition_AUTHOR",
                table: "ControlReqNota",
                column: "AUTHOR",
                principalTable: "UserPossition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedManipulation_Manipulation_ID_MANIPULADOS",
                table: "MedManipulation",
                column: "ID_MANIPULADOS",
                principalTable: "Manipulation",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductValidade_Vality_ID_VALIDADE",
                table: "ProductValidade",
                column: "ID_VALIDADE",
                principalTable: "Vality",
                principalColumn: "ID");
        }
    }
}
