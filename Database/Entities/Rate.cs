using CSharpFunctionalExtensions;

namespace WheelDeal.Database.Entities;

public class Rate
{
    public int Id { get; private set; }   
    public int UserId { get; set; }
    public string? Comment { get; set; }
    public DateTime Date { get; set; }
    public int? Points { get; set; }
}