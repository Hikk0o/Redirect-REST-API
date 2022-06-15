using System.ComponentModel.DataAnnotations.Schema;

namespace RedirectAPI.Data;

/// Database Entity
[Table("images")] public class Image
{
    [Column("id")] public int Id { get; set; }
    [Column("user_id"), ForeignKey("images_user_id_fkey")] public int UserId { get; set; }
    [Column("short_url")] public string ShortUrl { get; set; } = null!;
    /// Image in base64 converted to byte array
    [Column("img")] public byte[] Data { get; set; } = null!;
}