using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class opcoesCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescricaoSEO",
                table: "Categoria",
                type: "TEXT",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ExibirItemMenu",
                table: "Categoria",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TituloPaginaSEO",
                table: "Categoria",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescricaoSEO",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "ExibirItemMenu",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "TituloPaginaSEO",
                table: "Categoria");
        }
    }
}
