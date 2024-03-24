using Microsoft.EntityFrameworkCore;
using Twtr.Domain.UrlGeneration;
using Twtr.UrlShortener.Persistence;
using Twtr.UrlShortener.Persistence.UrlShortener;

namespace Twtr.UrlService
{
    public class UrlShortenerService : IUrlShortener
    {
        private readonly IShortUrlGenerator urlGenerator;
        private readonly IDbContextFactory<UrlShortenerContext> contextFactory;

        public UrlShortenerService(IDbContextFactory<UrlShortenerContext> dbContextFactory, IShortUrlGenerator shortUrlGenerator)
        {
            urlGenerator = shortUrlGenerator;
            contextFactory = dbContextFactory;
        }

        public async Task<string> GetShortUrl(Guid id)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var url = await context.UrlEntities.FindAsync(id);

            return $"{url?.TLDEntity?.TLD}/{url?.ShortenedUrl}";
        }

        public async Task<string> GetFullUrl(string shortenedUrl)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var url = await context.UrlEntities.SingleAsync(s => s.ShortenedUrl == shortenedUrl);

            return url?.OriginalUrl;
        }

        public async Task<Guid?> ShortenUrlRequest(string fullLengthUrl)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var twtrTLD = await context.TLDEntities.FirstOrDefaultAsync();

            var urlId = ShortenUrl(twtrTLD, fullLengthUrl);
        
            return urlId;
        }

        private Guid ShortenUrl(TLDEntity tldEntity, string fullLengthUrl)
        {
            var urlId = Guid.Empty;
            if (tldEntity is not null)
            {
                using var context = contextFactory.CreateDbContext();

                if (CheckIfUrlExists(tldEntity, fullLengthUrl).Result)
                {
                    urlId = context.UrlEntities.Single(url => url.TLDId == tldEntity.Id && url.OriginalUrl == fullLengthUrl).Id;
                }
                else
                {
                    tldEntity.SeedNumber++;
                    var shortUrl = urlGenerator.GenerateShortUrlKey(tldEntity.SeedNumber);

                    context.TLDEntities
                        .Where(w => w.Id == tldEntity.Id)
                        .ExecuteUpdate(u => u.SetProperty(s => s.SeedNumber, tldEntity.SeedNumber));

                    var urlEntity = new UrlEntity { OriginalUrl = fullLengthUrl, ShortenedUrl = shortUrl, TLDId = tldEntity?.Id };
                    context.UrlEntities.Add(urlEntity);
                    context.SaveChanges();

                    urlId = urlEntity.Id;
                }
            }
            return urlId;
        }

        public async Task<bool> CheckIfUrlExists(TLDEntity tldEntity, string fullUrl)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            return await context.UrlEntities.AnyAsync(url => url.TLDId == tldEntity.Id && url.OriginalUrl == fullUrl);
        }

        public async Task<List<TLDEntity>> GetTLDs()
        {
            using var context = await contextFactory.CreateDbContextAsync();

            return await context.TLDEntities.ToListAsync();
        }
    }
}
