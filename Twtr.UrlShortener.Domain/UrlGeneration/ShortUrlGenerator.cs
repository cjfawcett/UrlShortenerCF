using System.Text;

namespace Twtr.Domain.UrlGeneration
{
    public class ShortUrlGenerator : IShortUrlGenerator
    {
        private readonly string BASE62ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public int AlphabetLength => BASE62ALPHABET.Length;

        public string GenerateShortUrlKey(int? seedNumber)
        {
            var keyGenerator = new StringBuilder();

            while (seedNumber != 0)
            {
                int remainder = seedNumber.Value % 62;
                keyGenerator.Insert(0, BASE62ALPHABET[remainder]);
                seedNumber /= 62;
            }
                
            return keyGenerator.ToString();
        }
    }
}
