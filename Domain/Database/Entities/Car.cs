using CSharpFunctionalExtensions;

namespace WheelDeal.Domain.Database.Entities;

public class Car
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int PlacesCount { get; set; }
    public decimal EngineValue { get; set; }
    public int Mileage { get; set; }
    public string Body { get; set; }
    public string Fuel { get; set; }
    public string Transmission { get; set; }
    public decimal FuelConsumption { get; set; }
    public int Power { get; set; }

    // Навигационное свойство для Post
    public ICollection<Post> Posts { get; set; }
}
