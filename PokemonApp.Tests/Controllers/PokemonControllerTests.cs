using Microsoft.AspNetCore.Mvc;
using Moq;
using PokemonApp.Server.Controllers;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models.PokemonResponses;

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
            var pokemonResponse = new PokemonResponse { Name = "pikachu" };

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