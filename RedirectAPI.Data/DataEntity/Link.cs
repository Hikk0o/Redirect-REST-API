using System.ComponentModel.DataAnnotations.Schema;

namespace RedirectAPI.Data;

/// Database Entity
[Table("links")] public class Link
{
    [Column("id")] public int Id { get; set; }
    [Column("user_id"), ForeignKey("fk_user_id_users")] public int UserId { get; set; }
    [Column("short_url")] public string ShortUrl { get; set; } = null!;
    [Column("long_url")] public string LongUrl { get; set; } = null!;
}
