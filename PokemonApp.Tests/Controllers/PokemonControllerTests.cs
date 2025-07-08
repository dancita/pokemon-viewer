using Microsoft.AspNetCore.Mvc;
using Moq;
using PokemonApp.Server.Controllers;
using PokemonApp.Server.Exceptions;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models.PokemonResponses;
using System.Net;

namespace PokemonApp.Tests.Controllers
{
    public class PokemonControllerTests
    {
        private readonly Mock<IPokemonInfoService>? _pokemonInfoServiceMock;
        private readonly Mock<IPokemonDbService>? _pokemonDbServiceMock;
        private readonly PokemonController? _controller;

        public PokemonControllerTests()
        {
            _pokemonInfoServiceMock = new Mock<IPokemonInfoService>();
            _pokemonDbServiceMock = new Mock<IPokemonDbService>();

            _controller = new PokemonController(
                _pokemonInfoServiceMock.Object,
                _pokemonDbServiceMock.Object
            );
        }

        [Fact]
        public async Task GetPokemon_WithCorrectIdentifier_ReturnsOk()
        {
            var pokemonResponse = PokemonTestData.GetSamplePokemon();

            _pokemonInfoServiceMock!
                .Setup(s => s.GetPokemonAsync("pikachu"))
                .ReturnsAsync(pokemonResponse);

            _pokemonDbServiceMock!
                .Setup(s => s.SavePokemonAsync(pokemonResponse))
                .Returns(Task.CompletedTask);

            var result = await _controller!.GetPokemon("pikachu");

            _pokemonInfoServiceMock.Verify(s => s.GetPokemonAsync("pikachu"), Times.Once);
            _pokemonDbServiceMock.Verify(s => s.SavePokemonAsync(pokemonResponse), Times.Once);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPokemon = Assert.IsType<PokemonResponse>(okResult.Value);
            Assert.Equal("pikachu", returnedPokemon.Name);
            Assert.Equal(1, returnedPokemon.Id);
            Assert.Equal(55, returnedPokemon.Height);
            Assert.Equal(120, returnedPokemon.Weight);
            Assert.Equal("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/25.png", returnedPokemon.Sprites?.FrontDefaultImage);
            Assert.Equal(2, returnedPokemon.Abilities?.Count);
            Assert.Equal("synchronize", returnedPokemon.Abilities[0].Ability?.Name);
            Assert.Equal("https://pokeapi.co/api/v2/ability/28/", returnedPokemon.Abilities[0].Ability?.Url);
            Assert.False(returnedPokemon.Abilities[0].IsHidden);
            Assert.Equal(1, returnedPokemon.Abilities[0].Slot);
            Assert.Equal("inner-focus", returnedPokemon.Abilities[1].Ability?.Name);
            Assert.Equal("https://pokeapi.co/api/v2/ability/39/", returnedPokemon.Abilities[1].Ability?.Url);
            Assert.True(returnedPokemon.Abilities[1].IsHidden);
            Assert.Equal(2, returnedPokemon.Abilities[1].Slot);
            Assert.Equal(1, returnedPokemon.Types?.Count);
            Assert.Equal(1, returnedPokemon.Types[0].Slot);
            Assert.Equal("Fire", returnedPokemon.Types[0]?.Type?.Name);
            Assert.Equal("https://pokeapi.co/api/v2/type/10/", returnedPokemon.Types[0]?.Type?.Url);
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task GetPokemon_PokemonInfoServiceThrowsException_ThrowsExceptionWithStatusCode(HttpStatusCode httpStatusCode)
        {
            var invalidPokemonName = "pikapika";
            _pokemonInfoServiceMock!
                .Setup(s => s.GetPokemonAsync(invalidPokemonName))
                .ThrowsAsync(new PokemonInfoException(httpStatusCode));

            var exception = await Assert.ThrowsAsync<PokemonInfoException>(() =>
                _controller!.GetPokemon(invalidPokemonName));

            Assert.Equal(httpStatusCode, exception.StatusCode);
            _pokemonInfoServiceMock.Verify(s => s.GetPokemonAsync(invalidPokemonName), Times.Once);
        }

        [Fact]
        public async Task GetPokemon_PokemonDbServiceThrowsException_ThrowsException()
        {
            var pokemonName = "pikachu";
            var pokemonResponse = PokemonTestData.GetSamplePokemon();

            _pokemonInfoServiceMock!
                .Setup(s => s.GetPokemonAsync(pokemonName))
                .ReturnsAsync(pokemonResponse);

            _pokemonDbServiceMock!
                .Setup(s => s.SavePokemonAsync(pokemonResponse))
                .ThrowsAsync(new PokemonInfoException(HttpStatusCode.InternalServerError, 
                    "Error happened while saving to the DB."));

            var exception = await Assert.ThrowsAsync<PokemonInfoException>(() =>
                _controller!.GetPokemon(pokemonName));

            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
            Assert.Equal("Error happened while saving to the DB.", exception.Message);
            _pokemonInfoServiceMock.Verify(s => s.GetPokemonAsync(pokemonName), Times.Once);
            _pokemonDbServiceMock.Verify(s => s.SavePokemonAsync(pokemonResponse), Times.Once);
        }

        [Theory]
        [InlineData("pikachu***")]
        [InlineData("Mr.Pipe")]
        [InlineData("Farfetch'd")]
        [InlineData("Mime Jr.")]
        public async Task GetPokemon_InvalidFormat_ReturnsBadRequest(string invalidIdentifier)
        {
            var result = await _controller!.GetPokemon(invalidIdentifier);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Only letters, numbers and hyphens are allowed.", badRequest.Value);
        }

        [Theory]
        [InlineData("1234567890123")]
        [InlineData("pikachuuuuuuu")]
        [InlineData("77777777777777777777")]
        public async Task GetPokemon_TooLongIdentifier_ReturnsBadRequest(string tooLongIdentifier)
        {
            var result = await _controller!.GetPokemon(tooLongIdentifier);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Maximum 12 characters are allowed.", badRequest.Value);
        }
    }
}