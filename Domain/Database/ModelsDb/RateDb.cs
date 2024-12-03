using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Domain.Database.ModelsDb;

[Table("rates")]
public class RateDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("userid")]
    public Guid UserId { get; set; }

    [ForeignKey("userid")]
    public UserDb User { get; set; } // Навигационное свойство

    [Column("postid")]
    public Guid PostId { get; set; }

    [ForeignKey("postid")]
    public PostDb Post { get; set; } // Навигационное свойство

    [Column("comment")]
    public string Comment { get; set; }

    [Column("points")]
    public int Points { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }
}
