using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Domain.Database.ModelsDb;

[Table("posts")]
public class PostDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("carid")]
    public Guid CarId { get; set; } // Внешний ключ на CarDb

    public CarDb Car { get; set; } // Навигационное свойство

    [Column("categoryid")]
    public Guid CategoryId { get; set; } // Внешний ключ на CategoryDb

    public CategoryDb Category { get; set; } // Навигационное свойство

    [Column("description")]
    public string Description { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("availabilitystatus")]
    public bool AvailabilityStatus { get; set; }

    [Column("createdat")]
    public DateTime CreatedAt { get; set; }

    public ICollection<RateDb> Rates { get; set; }
    
    [Column("imagespaths")]
    public ICollection<string> ImagesPaths { get; set; }
}
