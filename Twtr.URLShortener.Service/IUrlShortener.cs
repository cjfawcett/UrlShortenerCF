using Twtr.UrlShortener.Persistence;
using Twtr.UrlShortener.Persistence.UrlShortener;

namespace Twtr.UrlService
{
    public interface IUrlShortener
    {
        Task<List<TLDEntity>> GetTLDs();
        Task<string> GetUrl(Guid id);
        Task<Guid?> ShortenUrlRequest(string fullLengthUrl);
        Task<bool> CheckIfUrlExists(TLDEntity entity, string fullUrl);
    }
}