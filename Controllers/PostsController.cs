using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WheelDeal.Domain.Database;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Database.Storage;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Domain.ViewModels.Categories;
using WheelDeal.Domain.ViewModels.Posts;
using WheelDeal.Service.Interfaces;
using WheelDeal.Service.Realizations;

namespace WheelDeal.Controllers;

public class PostsController : Controller
{
    private readonly IPostService _postService;
    private readonly IBaseStorage<CarDb> _carStorage;

    public PostsController(IPostService postService, IBaseStorage<CarDb> carStorage)
    {
        _postService = postService;
        _carStorage = carStorage;
    }

    public IActionResult ListOfPosts(Guid id)
    {
        var result = _postService.GetAllPostsByIdCategory(id);

        var viewModel = new ListOfPostsViewModel
        {
            Posts = result.Data.Select(post => new PostForPostsViewModel
            {
                Id = post.Id,
                CarId = post.CarId,
                CategoryId = post.CategoryId,
                Description = post.Description,
                Price = post.Price,
                AvailabilityStatus = post.AvailabilityStatus,
                CreatedAt = post.CreatedAt,
                Stars = post.Stars,
                ImagesPaths = post.ImagesPaths,
                Car = _carStorage.GetAll().Where(c => c.Id == post.CarId).ToList()[0]
            }).ToList(),
            CategoryId = id
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Filter([FromBody] PostFilter filter)
    {
        // Получаем результат фильтрации
        var result = _postService.GetPostByFilter(filter);

        // Ручное преобразование данных в список PostForPostsViewModel
        var filteredPosts = result.Data.Select(post => new PostForPostsViewModel
        {
            Id = post.Id,
            CarId = post.CarId,
            CategoryId = post.CategoryId,
            Description = post.Description,
            Price = post.Price,
            AvailabilityStatus = post.AvailabilityStatus,
            CreatedAt = post.CreatedAt,
            Stars = post.Stars,
            ImagesPaths = post.ImagesPaths,
            Car = _carStorage.GetAll().FirstOrDefault(c => c.Id == post.CarId) // Получение машины по ID
        }).ToList();

        // Возвращаем JSON-ответ
        return Json(filteredPosts);
    }
}
