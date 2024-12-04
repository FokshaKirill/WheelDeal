namespace WheelDeal.Domain.Database.Entities;

public class Post
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; } // Внешний ключ на CarDb
    public Car Car { get; set; } // Навигационное свойство
    public Guid CategoryId { get; set; } // Внешний ключ на CategoryDb
    public Category Category { get; set; } // Навигационное свойство
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool AvailabilityStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Rate> Rates { get; set; }
    public ICollection<string> ImagesPaths { get; set; }
}
