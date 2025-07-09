using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPokemonApiId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PokemonId",
                table: "PokemonRequest",
                newName: "PokemonApiId");

            migrationBuilder.AddColumn<int>(
                name: "PokemonApiId",
                table: "Pokemon",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PokemonApiId",
                table: "Pokemon");

            migrationBuilder.RenameColumn(
                name: "PokemonApiId",
                table: "PokemonRequest",
                newName: "PokemonId");
        }
    }
}
