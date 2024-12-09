namespace WheelDeal.Domain.Database.Entities;

public class PostFilter
{
    public Guid CategoryId { get; set; }
    public decimal PriceMin { get; set; }
    public decimal PriceMax { get; set; }
    public List<string> FuelTypes { get; set; }
}