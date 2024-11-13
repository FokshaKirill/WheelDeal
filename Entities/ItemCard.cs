using CSharpFunctionalExtensions;

namespace WheelDeal.Entities;

public class ItemCard
{
    public int Id { get; private set; } 
    public string? Description { get; set; }
    public decimal Price { get; set; }
    
    // public Car Car { get; set; }
    // public int CarID { get; set; }
    //
    // public List<int> CommsID { get; set; }
    // public List<int> Rates { get; set; }
    // public List<int> ImgsID { get; set; }
}