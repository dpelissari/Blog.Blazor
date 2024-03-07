using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class AddBiogriaAutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biografia",
                table: "Autor",
                type: "TEXT",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biografia",
                table: "Autor");
        }
    }
}
