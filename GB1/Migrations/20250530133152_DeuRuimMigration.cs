using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GB1.Migrations
{
    /// <inheritdoc />
    public partial class DeuRuimMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "DESASTRE_SEQ",
                startValue: 10L);

            migrationBuilder.CreateSequence(
                name: "USUARIO_SEQ",
                startValue: 10L);

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "USUARIO_SEQ.NEXTVAL"),
                    USERNAME = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    UF = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    NIVEL = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DESASTRE",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "DESASTRE_SEQ.NEXTVAL"),
                    TITULO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    DATA_DESASTRE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    SEVERIDADE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    UsuarioId = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DESASTRE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DESASTRE_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DESASTRE_UsuarioId",
                table: "DESASTRE",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DESASTRE");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropSequence(
                name: "DESASTRE_SEQ");

            migrationBuilder.DropSequence(
                name: "USUARIO_SEQ");
        }
    }
}
