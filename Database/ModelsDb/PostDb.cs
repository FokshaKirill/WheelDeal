using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Database.ModelsDb;

[Table("posts")]
public class PostDb
{
    [Column("id")]
    public Guid Id { get; private set; } 
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }

    [Column("id_rates")]
    public List<int>? RatesID { get; set; }
    
    [Column("id_car")]
    public int CarID { get; set; }  
}