using Twtr.Domain.UrlGeneration;

namespace Twtr.UrlShortener.Tests
{
    public class ShortUrlGeneratorTests
    {
        [Fact]
        public void The_Base62_Alphabet_Returns_The_Correct_Length()
        {
            var generator = new ShortUrlGenerator();
            var result = generator.AlphabetLength;
            Assert.Equal(62, result);
        }

        [Theory]
        [InlineData(343, "5X")]
        [InlineData(1000, "G8")]
        [InlineData(1000000, "4C92")]
        [InlineData(10000000, "fxSK")]
        public void GenerateShortUrlKey_Returns_The_Expected_Encoding(int seedNumber, string base62Representation)
        {
            var generator = new ShortUrlGenerator();
            var result = generator.GenerateShortUrlKey(seedNumber);
            Assert.Equal(base62Representation, result);
        }
    }
}