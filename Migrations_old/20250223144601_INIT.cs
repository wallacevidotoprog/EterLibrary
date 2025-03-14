using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EterLibrary.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    RG = table.Column<string>(type: "TEXT", nullable: true),
                    NOME = table.Column<string>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMethod",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAME = table.Column<string>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethod", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAME = table.Column<string>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NOME = table.Column<string>(type: "TEXT", nullable: true),
                    PERMISSION = table.Column<int>(type: "INTEGER", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Situation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAME = table.Column<string>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CLIENT = table.Column<long>(type: "INTEGER", nullable: true),
                    PLACE = table.Column<string>(type: "TEXT", nullable: true),
                    NUMBER = table.Column<int>(type: "INTEGER", nullable: true),
                    ZONE = table.Column<string>(type: "TEXT", nullable: true),
                    CITY = table.Column<string>(type: "TEXT", nullable: true),
                    UF = table.Column<string>(type: "TEXT", nullable: true),
                    OBSERVACAO = table.Column<string>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Address_Client_ID_CLIENT",
                        column: x => x.ID_CLIENT,
                        principalTable: "Client",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserPossition",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_LOJA = table.Column<long>(type: "INTEGER", nullable: true),
                    NOME = table.Column<string>(type: "TEXT", nullable: true),
                    PASS = table.Column<string>(type: "TEXT", nullable: true),
                    ID_FUNCAO = table.Column<long>(type: "INTEGER", nullable: true),
                    STATUS = table.Column<bool>(type: "INTEGER", nullable: false),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPossition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPossition_Position_ID_FUNCAO",
                        column: x => x.ID_FUNCAO,
                        principalTable: "Position",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAME = table.Column<string>(type: "TEXT", nullable: true),
                    ID_USER = table.Column<long>(type: "INTEGER", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Category_UserPossition_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "UserPossition",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ControlReqNota",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VENDEDOR = table.Column<long>(type: "INTEGER", nullable: true),
                    AUTHOR = table.Column<long>(type: "INTEGER", nullable: true),
                    DATA_VENDA = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DATA_ENVIO = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlReqNota", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ControlReqNota_UserPossition_AUTHOR",
                        column: x => x.AUTHOR,
                        principalTable: "UserPossition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlReqNota_UserPossition_VENDEDOR",
                        column: x => x.VENDEDOR,
                        principalTable: "UserPossition",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Manipulation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ATEN_LOJA = table.Column<long>(type: "INTEGER", nullable: true),
                    DATA = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ATEN_MANI = table.Column<string>(type: "TEXT", nullable: true),
                    ID_CLIENTE = table.Column<long>(type: "INTEGER", nullable: true),
                    ID_ENDERECO = table.Column<long>(type: "INTEGER", nullable: true),
                    ID_SITUCAO = table.Column<long>(type: "INTEGER", nullable: true),
                    ID_FORMAPAGAMENTO = table.Column<long>(type: "INTEGER", nullable: true),
                    ID_MODOENTREGA = table.Column<long>(type: "INTEGER", nullable: true),
                    VALORFINAL = table.Column<decimal>(type: "TEXT", nullable: true),
                    OBSGERAL = table.Column<string>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manipulation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Manipulation_Address_ID_ENDERECO",
                        column: x => x.ID_ENDERECO,
                        principalTable: "Address",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Manipulation_Client_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "Client",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Manipulation_DeliveryMethod_ID_MODOENTREGA",
                        column: x => x.ID_MODOENTREGA,
                        principalTable: "DeliveryMethod",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Manipulation_Payment_ID_FORMAPAGAMENTO",
                        column: x => x.ID_FORMAPAGAMENTO,
                        principalTable: "Payment",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Manipulation_Situation_ID_SITUCAO",
                        column: x => x.ID_SITUCAO,
                        principalTable: "Situation",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Manipulation_UserPossition_ATEN_LOJA",
                        column: x => x.ATEN_LOJA,
                        principalTable: "UserPossition",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MedControl",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CLIENT = table.Column<long>(type: "INTEGER", nullable: true),
                    ID_ADDRESS = table.Column<long>(type: "INTEGER", nullable: true),
                    ID_USER = table.Column<long>(type: "INTEGER", nullable: true),
                    CODIGO = table.Column<int>(type: "INTEGER", nullable: true),
                    NAME_M = table.Column<string>(type: "TEXT", nullable: true),
                    QTD = table.Column<int>(type: "INTEGER", nullable: false),
                    VALIDADE = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LOTE = table.Column<string>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedControl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedControl_Address_ID_ADDRESS",
                        column: x => x.ID_ADDRESS,
                        principalTable: "Address",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedControl_Client_ID_CLIENT",
                        column: x => x.ID_CLIENT,
                        principalTable: "Client",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedControl_UserPossition_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "UserPossition",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Vality",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_USER = table.Column<long>(type: "INTEGER", nullable: true),
                    DATE = table.Column<long>(type: "INTEGER", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vality", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vality_UserPossition_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "UserPossition",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ReqNota",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CQN_ID = table.Column<long>(type: "INTEGER", nullable: true),
                    REQ = table.Column<string>(type: "TEXT", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReqNota", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReqNota_ControlReqNota_CQN_ID",
                        column: x => x.CQN_ID,
                        principalTable: "ControlReqNota",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MedManipulation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_MANIPULADOS = table.Column<long>(type: "INTEGER", nullable: true),
                    NAME_M = table.Column<string>(type: "TEXT", nullable: true),
                    ManipulationID = table.Column<long>(type: "INTEGER", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedManipulation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedManipulation_Manipulation_ManipulationID",
                        column: x => x.ManipulationID,
                        principalTable: "Manipulation",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductValidade",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_VALIDADE = table.Column<long>(type: "INTEGER", nullable: true),
                    PRODUTO_CODIGO = table.Column<int>(type: "INTEGER", nullable: true),
                    PRODUTO_DESCRICAO = table.Column<string>(type: "TEXT", nullable: true),
                    QUANTIDADE = table.Column<int>(type: "INTEGER", nullable: true),
                    ID_CATEGORIA = table.Column<long>(type: "INTEGER", nullable: true),
                    DATA_VALIDADE = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ValityID = table.Column<long>(type: "INTEGER", nullable: true),
                    CREATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UPDATE_AT = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductValidade", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductValidade_Category_ID_CATEGORIA",
                        column: x => x.ID_CATEGORIA,
                        principalTable: "Category",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductValidade_Vality_ValityID",
                        column: x => x.ValityID,
                        principalTable: "Vality",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_ID_CLIENT",
                table: "Address",
                column: "ID_CLIENT");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ID_USER",
                table: "Category",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_ControlReqNota_AUTHOR",
                table: "ControlReqNota",
                column: "AUTHOR");

            migrationBuilder.CreateIndex(
                name: "IX_ControlReqNota_VENDEDOR",
                table: "ControlReqNota",
                column: "VENDEDOR");

            migrationBuilder.CreateIndex(
                name: "IX_Manipulation_ATEN_LOJA",
                table: "Manipulation",
                column: "ATEN_LOJA");

            migrationBuilder.CreateIndex(
                name: "IX_Manipulation_ID_CLIENTE",
                table: "Manipulation",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_Manipulation_ID_ENDERECO",
                table: "Manipulation",
                column: "ID_ENDERECO");

            migrationBuilder.CreateIndex(
                name: "IX_Manipulation_ID_FORMAPAGAMENTO",
                table: "Manipulation",
                column: "ID_FORMAPAGAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_Manipulation_ID_MODOENTREGA",
                table: "Manipulation",
                column: "ID_MODOENTREGA");

            migrationBuilder.CreateIndex(
                name: "IX_Manipulation_ID_SITUCAO",
                table: "Manipulation",
                column: "ID_SITUCAO");

            migrationBuilder.CreateIndex(
                name: "IX_MedControl_ID_ADDRESS",
                table: "MedControl",
                column: "ID_ADDRESS");

            migrationBuilder.CreateIndex(
                name: "IX_MedControl_ID_CLIENT",
                table: "MedControl",
                column: "ID_CLIENT");

            migrationBuilder.CreateIndex(
                name: "IX_MedControl_ID_USER",
                table: "MedControl",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_MedManipulation_ManipulationID",
                table: "MedManipulation",
                column: "ManipulationID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductValidade_ID_CATEGORIA",
                table: "ProductValidade",
                column: "ID_CATEGORIA");

            migrationBuilder.CreateIndex(
                name: "IX_ProductValidade_ValityID",
                table: "ProductValidade",
                column: "ValityID");

            migrationBuilder.CreateIndex(
                name: "IX_ReqNota_CQN_ID",
                table: "ReqNota",
                column: "CQN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPossition_ID_FUNCAO",
                table: "UserPossition",
                column: "ID_FUNCAO");

            migrationBuilder.CreateIndex(
                name: "IX_Vality_ID_USER",
                table: "Vality",
                column: "ID_USER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedControl");

            migrationBuilder.DropTable(
                name: "MedManipulation");

            migrationBuilder.DropTable(
                name: "ProductValidade");

            migrationBuilder.DropTable(
                name: "ReqNota");

            migrationBuilder.DropTable(
                name: "Manipulation");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Vality");

            migrationBuilder.DropTable(
                name: "ControlReqNota");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "DeliveryMethod");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Situation");

            migrationBuilder.DropTable(
                name: "UserPossition");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Position");
        }
    }
}
