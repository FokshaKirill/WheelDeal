using WheelDeal.Domain.Database.ModelsDb;

namespace WheelDeal.Domain.ViewModels.Posts;

public class ListOfPostsViewModel
{
    public List<PostForPostsViewModel> Posts { get; set; }
    public Guid CategoryId { get; set; }
}

public class PostForPostsViewModel
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    
    public Guid CarId { get; set; } // Внешний ключ на CarDb

    public CarDb Car { get; set; } // Навигационное свойство
    public int? Year { get; set; }
    public int Stars { get; set; }
    public ICollection<string> ImagesPaths { get; set; }
    public decimal Price { get; set; }

}