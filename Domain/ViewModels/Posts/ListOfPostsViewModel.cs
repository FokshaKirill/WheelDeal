using WheelDeal.Domain.Database.Entities;
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
    public Guid CarId { get; set; }
    public Guid CategoryId { get; set; }
    public CarDb Car { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool AvailabilityStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stars { get; set; }
    public ICollection<string> ImagesPaths { get; set; }
}