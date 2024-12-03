using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WheelDeal.Domain.Database;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Domain.ViewModels.Categories;
using WheelDeal.Domain.ViewModels.Posts;
using WheelDeal.Service.Interfaces;
using WheelDeal.Service.Realizations;

namespace WheelDeal.Controllers;

public class PostsController : Controller
{
    private readonly IPostService _postService;
    
    public IMapper _mapper { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public PostsController(IPostService postService)
    {
        _postService = postService;
        _mapper = mapperConfiguration.CreateMapper();
    }

    public IActionResult ListOfPosts(Guid id)
    {
        var result = _postService.GetAllPostsByIdCategory(id);
        ListOfPostsViewModel listPosts = new ListOfPostsViewModel
        {
            Posts = _mapper.Map<List<PostForPostsViewModel>>(result.Data),
            CategoryId = id
        };
        
        return View(listPosts);
    }
}