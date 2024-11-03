using CSharpFunctionalExtensions;

namespace WheelDeal.Entities;

public class Car : Entity
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int CarItemId { get; set; }
    public int PlacesCount { get; set; }
    public int EngineValue { get; set; }
    public int Mileage { get; set; }
    public string Body { get; set; }    
    public string Fuel { get; set; }
    public string Transmission { get; set; }
    public decimal FuelConsumption { get; set; }
    public decimal Power { get; set; }

}