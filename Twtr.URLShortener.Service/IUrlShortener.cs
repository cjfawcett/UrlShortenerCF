using Twtr.UrlShortener.Persistence;

namespace Twtr.UrlService
{
    public interface IUrlShortener
    {
        Task<List<TLDEntity>> GetTLDs();
        Task<string> GetShortUrl(Guid id);
        Task<string> GetFullUrl(string shortenedUrl);
        Task<Guid?> ShortenUrlRequest(string fullLengthUrl);
        Task<bool> CheckIfUrlExists(TLDEntity entity, string fullUrl);
    }
}