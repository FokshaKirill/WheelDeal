using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Domain.Database.ModelsDb;

[Table("users")]
public class UserDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("login")]
    public string Login { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("role")]
    public int Role { get; set; }

    [Column("pathimage")]
    public string ImagePath { get; set; }

    [Column("createdat")]
    public DateTime CreatedAt { get; set; }

    // Навигационное свойство для RateDb
    public ICollection<RateDb> Rates { get; set; }
}
