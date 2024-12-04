namespace WheelDeal.Domain.Database.Entities;

public class Rate
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // Внешний ключ на UserDb
    public User User { get; set; } // Навигационное свойство
    public Guid PostId { get; set; } // Внешний ключ на PostDb
    public Post Post { get; set; } // Навигационное свойство
    public string Comment { get; set; }
    public int Points { get; set; }
    public DateTime Date { get; set; }
}