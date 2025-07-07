using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PokemonApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(12)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    FrontDefaultImage = table.Column<string>(type: "nvarchar(2048)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => new { x.PokemonId, x.AbilityId });
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Ability_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => new { x.PokemonId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_PokemonTypes_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonTypes_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Normal", "https://pokeapi.co/api/v2/type/1/" },
                    { 2, "Fighting", "https://pokeapi.co/api/v2/type/2/" },
                    { 3, "Flying", "https://pokeapi.co/api/v2/type/3/" },
                    { 4, "Poison", "https://pokeapi.co/api/v2/type/4/" },
                    { 5, "Ground", "https://pokeapi.co/api/v2/type/5/" },
                    { 6, "Rock", "https://pokeapi.co/api/v2/type/6/" },
                    { 7, "Bug", "https://pokeapi.co/api/v2/type/7/" },
                    { 8, "Ghost", "https://pokeapi.co/api/v2/type/8/" },
                    { 9, "Steel", "https://pokeapi.co/api/v2/type/9/" },
                    { 10, "Fire", "https://pokeapi.co/api/v2/type/10/" },
                    { 11, "Water", "https://pokeapi.co/api/v2/type/11/" },
                    { 12, "Grass", "https://pokeapi.co/api/v2/type/12/" },
                    { 13, "Electric", "https://pokeapi.co/api/v2/type/13/" },
                    { 14, "Psychic", "https://pokeapi.co/api/v2/type/14/" },
                    { 15, "Ice", "https://pokeapi.co/api/v2/type/15/" },
                    { 16, "Dragon", "https://pokeapi.co/api/v2/type/16/" },
                    { 17, "Dark", "https://pokeapi.co/api/v2/type/17/" },
                    { 18, "Fairy", "https://pokeapi.co/api/v2/type/18/" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_TypeId",
                table: "PokemonTypes",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "PokemonTypes");

            migrationBuilder.DropTable(
                name: "Ability");

            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
