using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Domain.Database.ModelsDb;

[Table("cars")]
public class CarDb
{
    [Column("id")]
    public Guid Id { get; private set; } 
    
    [Column("brand")]
    public string Brand { get; set; }
    
    [Column("model")]
    public string Model { get; set; }
    
    [Column("year")]
    public int Year { get; set; }
    
    [Column("placesCount")]
    public int PlacesCount { get; set; }
    
    [Column("engineValue")]
    public int EngineValue { get; set; }
    
    [Column("mileage")]
    public int Mileage { get; set; }
    
    [Column("body")]
    public string Body { get; set; }    
    
    [Column("fuel")]
    public string Fuel { get; set; }
    
    [Column("transmission")]
    public string Transmission { get; set; }
    
    [Column("fuelConsumption")]
    public decimal FuelConsumption { get; set; }
    
    [Column("power")]
    public decimal Power { get; set; }
}