using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPokemonRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Ability_AbilityId",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTypes_Type_TypeId",
                table: "PokemonTypes");

            migrationBuilder.AddColumn<int>(
                name: "AbilityApiId",
                table: "Ability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PokemonRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonRequest", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Ability_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId",
                principalTable: "Ability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTypes_Type_TypeId",
                table: "PokemonTypes",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Ability_AbilityId",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTypes_Type_TypeId",
                table: "PokemonTypes");

            migrationBuilder.DropTable(
                name: "PokemonRequest");

            migrationBuilder.DropColumn(
                name: "AbilityApiId",
                table: "Ability");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Ability_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId",
                principalTable: "Ability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTypes_Type_TypeId",
                table: "PokemonTypes",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
