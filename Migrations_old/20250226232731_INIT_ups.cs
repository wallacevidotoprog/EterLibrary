using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EterLibrary.Migrations
{
    /// <inheritdoc />
    public partial class INIT_ups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductValidade_Vality_ValityID",
                table: "ProductValidade");

            migrationBuilder.DropIndex(
                name: "IX_ProductValidade_ValityID",
                table: "ProductValidade");

            migrationBuilder.DropColumn(
                name: "ValityID",
                table: "ProductValidade");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE",
                table: "Vality",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PHONE",
                table: "Client",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductValidade_ID_VALIDADE",
                table: "ProductValidade",
                column: "ID_VALIDADE");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductValidade_Vality_ID_VALIDADE",
                table: "ProductValidade",
                column: "ID_VALIDADE",
                principalTable: "Vality",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductValidade_Vality_ID_VALIDADE",
                table: "ProductValidade");

            migrationBuilder.DropIndex(
                name: "IX_ProductValidade_ID_VALIDADE",
                table: "ProductValidade");

            migrationBuilder.DropColumn(
                name: "PHONE",
                table: "Client");

            migrationBuilder.AlterColumn<long>(
                name: "DATE",
                table: "Vality",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ValityID",
                table: "ProductValidade",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductValidade_ValityID",
                table: "ProductValidade",
                column: "ValityID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductValidade_Vality_ValityID",
                table: "ProductValidade",
                column: "ValityID",
                principalTable: "Vality",
                principalColumn: "ID");
        }
    }
}
