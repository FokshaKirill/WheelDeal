using CSharpFunctionalExtensions;

namespace WheelDeal.Database.Entities;

public class Post
{
    public int Id { get; private set; } 
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public List<int> RatesID { get; set; }
    public int CarID { get; set; }    

    // public Car Car { get; set; }

    //
    // public List<int> CommsID { get; set; }
    // public List<int> ImgsID { get; set; }
}