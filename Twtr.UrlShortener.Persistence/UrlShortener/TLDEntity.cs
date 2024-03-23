using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twtr.UrlShortener.Persistence
{
    [Table("TLDs")]
    public class TLDEntity
    {
        [Key]
        [Column]
        public Guid? Id { get; set; }

        [Column]
        public string? Name { get; set; }

        [Column]
        public string? TLD {  get; set; }

        [Column]
        public int? SeedNumber { get; set; }
    }
}
