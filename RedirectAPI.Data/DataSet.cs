using System.ComponentModel.DataAnnotations.Schema;

namespace RedirectAPI.Data;

[Table("users")] public class User
{
    [Column("id")] public int Id { get; set; }

    [Column("ip")] public string Ip { get; set; } = null!;

    public override string ToString()
    {
        return $"{Id}: {Ip}\n";
    }
}

[Table("links")] public class Link
{
    [Column("id"), ForeignKey("fk_user_id_users")] public int Id { get; set; }
    [Column("user_id")] public int UserId { get; set; }
    [Column("short_url")] public string ShortUrl { get; set; } = null!;
    [Column("long_url")] public string LongUrl { get; set; } = null!;
}