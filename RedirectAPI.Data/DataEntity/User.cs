using System.ComponentModel.DataAnnotations.Schema;

namespace RedirectAPI.Data;

/// Database Entity
[Table("users")] public class User
{ 
    [Column("id")] public int Id { get; set; }
    [Column("ip")] public string Ip { get; set; } = null!;

    /// <returns>sting entity</returns>
    public override string ToString()
    {
        return $"{Id}: {Ip}";
    }
}
