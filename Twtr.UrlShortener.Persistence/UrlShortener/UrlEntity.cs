using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twtr.UrlShortener.Persistence.UrlShortener
{
    [Table("Urls")]
    [Index(nameof(TLDId), nameof(ShortenedUrl), IsUnique = true, Name = "IX_TLDId_ShortenedUrl")]
    public class UrlEntity
    {
        [Key]
        [Column]
        public Guid Id { get; set; }

        [Column]
        public Guid? TLDId { get; set; }

        [Column]
        public string? OriginalUrl { get; set; }

        [Column]
        public string? ShortenedUrl {  get; set; }

        [ForeignKey("TLDId")]
        public virtual TLDEntity? TLDEntity { get; set; }
    }
}
