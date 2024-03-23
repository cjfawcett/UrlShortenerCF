using Microsoft.EntityFrameworkCore;
using Twtr.UrlShortener.Persistence;

namespace Twtr.WebApp
{
    public static class DataSeeder
    {
        public static void SeedDB(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetService<UrlShortenerContext>();
               
                if (context is not null)
                {
                    context.Database.Migrate();

                    if (!context.TLDEntities.Any())
                    {
                        var twitterTLD = new TLDEntity { Id = Guid.NewGuid(), Name = "TWTR", TLD = "https://twtr.co", SeedNumber = 10000000 };
                        context.TLDEntities.Add(twitterTLD);

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
