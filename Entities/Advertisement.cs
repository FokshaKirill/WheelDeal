using CSharpFunctionalExtensions;

namespace WheelDeal.Entities;

public class Advertisement
{
    public int ID { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Car Car { get; set; }
    public int CarID { get; set; }
    
    public List<int> CommsID { get; set; }
    public List<int> Rates { get; set; }
    public List<int> ImgsID { get; set; }
}