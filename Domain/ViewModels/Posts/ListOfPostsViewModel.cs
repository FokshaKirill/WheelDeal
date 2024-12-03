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
    public string Brand { get; set; }
    public string Model { get; set; }
    public int? Year { get; set; }
    public int PlacesCount { get; set; }
    public string Fuel { get; set; }
    public decimal FuelConsumption { get; set; }
    public decimal Power { get; set; }
    public int Stars { get; set; }
    public string ImagePath { get; set; }
    public decimal Price { get; set; }
}