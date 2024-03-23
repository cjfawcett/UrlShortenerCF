using Microsoft.EntityFrameworkCore;
using Twtr.UrlShortener.Persistence.UrlShortener;

namespace Twtr.UrlShortener.Persistence
{
    public class UrlShortenerContext: DbContext
    {

        public virtual DbSet<TLDEntity> TLDEntities { get; set; }
        public virtual DbSet<UrlEntity> UrlEntities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TLDEntity>();
            modelBuilder.Entity<UrlEntity>();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=URLShorteningDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
