namespace WheelDeal.Domain.Database.Entities;

public class Rate
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    public User User { get; set; }  // Навигационное свойство

    public Guid PostId { get; set; }
    
    public Post Post { get; set; }  // Навигационное свойство

    public string Comment { get; set; }
    public int Points { get; set; }
    public DateTime Date { get; set; }
}
