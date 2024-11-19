using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Database.ModelsDb;

[Table("rates")]
public class RateDb
{
    [Column("id")]
    public Guid Id { get; private set; } 
    
    [Column("id_user")]
    public string UserId { get; set; }
    
    [Column("comment")]
    public string? Comment { get; set; }
    
    [Column("year")]
    public int Points { get; set; }
    
    [Column("date")]
    public DateTime Date { get; set; }
}