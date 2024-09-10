using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoBanco : Migration
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
                    DescricaoSEO = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    TituloPaginaSEO = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ExibirItemMenu = table.Column<bool>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Conteudo = table.Column<string>(type: "TEXT", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DescricaoSEO = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    TituloPaginaSEO = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CaminhoImagem = table.Column<string>(type: "TEXT", nullable: false),
                    IdAutor = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCategoria = table.Column<Guid>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Autor_IdAutor",
                        column: x => x.IdAutor,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Cadastro", "Email", "SenhaHash", "Tipo" },
                values: new object[] { new Guid("c19ecfd1-fb62-41b9-81e2-8541cc8898c5"), new DateTime(2024, 7, 3, 10, 46, 2, 219, DateTimeKind.Local).AddTicks(9167), "root", "0ee484c7d01b93fe9ff6dec76671dda6e08d5d8c8a052f83db29fa898b8bacb1", 20 });

            migrationBuilder.CreateIndex(
                name: "IX_Post_IdAutor",
                table: "Post",
                column: "IdAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IdCategoria",
                table: "Post",
                column: "IdCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
