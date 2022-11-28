using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class imagensprojeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImagemProjeto",
                columns: table => new
                {
                    ImagemProjetoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Caminho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjetoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagemProjeto", x => x.ImagemProjetoId);
                    table.ForeignKey(
                        name: "FK_ImagemProjeto_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "ProjetoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagemProjeto_ProjetoId",
                table: "ImagemProjeto",
                column: "ProjetoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagemProjeto");
        }
    }
}
