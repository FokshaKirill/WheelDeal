using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Domain.Database.ModelsDb;

[Table("cars")]
public class CarDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("brand")]
    public string Brand { get; set; }

    [Column("model")]
    public string Model { get; set; }

    [Column("year")]
    public int Year { get; set; }

    [Column("placescount")]
    public int PlacesCount { get; set; }

    [Column("enginevalue")]
    public decimal EngineValue { get; set; }

    [Column("mileage")]
    public int Mileage { get; set; }

    [Column("body")]
    public string Body { get; set; }

    [Column("fuel")]
    public string Fuel { get; set; }

    [Column("transmission")]
    public string Transmission { get; set; }

    [Column("fuelconsumption")]
    public decimal FuelConsumption { get; set; }

    [Column("power")]
    public int Power { get; set; }

    // Навигационное свойство для PostDb
    public ICollection<PostDb> Posts { get; set; }
}
