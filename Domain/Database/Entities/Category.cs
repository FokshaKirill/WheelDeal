using WheelDeal.Domain.Database.ModelsDb;

namespace WheelDeal.Domain.Database.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string ImagePath { get; set; }
    public string Name { get; set; }
    public int CountPosts { get; set; }

    // Навигационное свойство для Post
    public ICollection<Post> Posts { get; set; }
}