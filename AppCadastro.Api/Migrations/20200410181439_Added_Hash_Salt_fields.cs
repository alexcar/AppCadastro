using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCadastro.Api.Migrations
{
    public partial class Added_Hash_Salt_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Usuario",
                maxLength: 90,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Usuario",
                maxLength: 90,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Usuario");
        }
    }
}
