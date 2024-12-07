namespace WheelDeal.Domain.Database.Entities;

public class Post
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool AvailabilityStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stars { get; set; } // Средний рейтинг
    public ICollection<string> ImagesPaths { get; set; }
}
