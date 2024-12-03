using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WheelDeal.Domain.ViewModels.Categories;
using WheelDeal.Service.Interfaces;
using WheelDeal.Service.Realizations;

namespace WheelDeal.Controllers;

public class CategoriesController : Controller
{
    private readonly IAutoParkService _autoParkService;

    private IMapper _mapper { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public CategoriesController(IAutoParkService autoParkService) 
    {
        _autoParkService = autoParkService;
        _mapper = mapperConfiguration.CreateMapper();
    }

    public IActionResult ListOfCategories()
    {
        var result = _autoParkService.GetAllCategories();
        var listOfPosts = _mapper.Map<List<CategoryViewModel>>(result.Data);
        
        return View(listOfPosts);
    }
}
