using Microsoft.AspNetCore.Mvc;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.ViewModels.Categories;
using WheelDeal.Service.Interfaces;

namespace WheelDeal.Controllers;

public class CategoriesController : Controller
{
    private readonly IAutoParkService _autoParkService;

    public CategoriesController(IAutoParkService autoParkService) 
    {
        _autoParkService = autoParkService;
    }

    public IActionResult ListOfCategories()
    {
        var result = _autoParkService.GetAllCategories();

        if (result == null || result.Data == null)
        {
            return View("Error"); // Обработка ошибок
        }

        // Ручной маппинг
        var listOfPosts = result.Data.Select(category => new CategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            ImagePath = category.ImagePath,
            CountPosts = category.CountPosts
        }).ToList();

        return View(listOfPosts);
    }
}