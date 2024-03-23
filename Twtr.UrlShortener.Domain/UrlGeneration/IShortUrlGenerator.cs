namespace Twtr.Domain.UrlGeneration
{
    public interface IShortUrlGenerator
    {
        string GenerateShortUrlKey(int? seedNumber);
    }
}