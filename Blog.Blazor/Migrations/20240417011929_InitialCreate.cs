using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Biografia = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Cadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CaminhoImagem = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Cadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Conteudo = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    AutorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Aprovado = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentario_Autor_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Conteudo = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Cadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DescricaoSEO = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    TituloPaginaSEO = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CaminhoImagem = table.Column<string>(type: "TEXT", nullable: false),
                    IdAutor = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCategoria = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Autor_Id",
                        column: x => x.Id,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_Categoria_Id",
                        column: x => x.Id,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_AutorId",
                table: "Comentario",
                column: "AutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
