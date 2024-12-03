namespace WheelDeal.Domain.ViewModels.Categories;

public class CategoryViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public int CountPosts { get; set; }
}