using CSharpFunctionalExtensions;

namespace WheelDeal.Entities;

public class Rate
{
    public string? Comment { get; set; }
    public int CarItemId { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public int? Points { get; set; }
}