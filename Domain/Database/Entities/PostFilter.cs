namespace WheelDeal.Domain.Database.Entities;

public class PostFilter
{
    public Guid IdCategory { get; set; }
    public decimal PriceMin { get; set; }
    public decimal PriceMax { get; set; }
    public List<string> FuelTypesList { get; set; }
}