using PokemonApp.Server.Extensions;

namespace PokemonApp.Tests.Extensions
{
    public class PokemonParameterExtensionsTests
    {
        [Theory]
        [InlineData("pikachu", true)]
        [InlineData("pikachu123", true)]
        [InlineData("pika-chu", true)]
        [InlineData("pika_chu", false)]
        [InlineData("pikachu@", false)] 
        [InlineData("*pikachu", false)] 
        [InlineData("£$%^&*", false)] 
        [InlineData("", false)]
        [InlineData("   ", false)]
        public void IsValidFormat_ReturnsExpected(string input, bool expected)
        {
            var result = input.IsValidFormat();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("pikachu", 12, true)]
        [InlineData("pikachu123456", 12, false)] 
        [InlineData("Tepig", 5, true)]
        [InlineData("Tepig", 4, false)]
        public void IsValidLength_WithMaxLengthProvided_ReturnsExpected(string input, int maxLength, bool expected)
        {
            var result = input.IsValidLength(maxLength);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Fletchinder", true)]
        [InlineData("Fletchinder11", false)]
        [InlineData("Crabominable", true)]
        [InlineData("Crabominable-", false)]
        public void IsValidLength_DefaultMaxLength_ReturnsExpected(string input, bool expected)
        {
            var result = input.IsValidLength();

            Assert.Equal(expected, result);
        }
    }
}