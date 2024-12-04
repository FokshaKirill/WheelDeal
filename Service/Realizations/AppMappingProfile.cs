using AutoMapper;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.ViewModels.Categories;
using WheelDeal.Domain.ViewModels.LogAndReg;
using WheelDeal.Domain.ViewModels.Posts;

namespace WheelDeal.Service.Realizations;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDb>().ReverseMap();
        CreateMap<User, LoginViewModel>().ReverseMap();
        CreateMap<User, RegisterViewModel>().ReverseMap();
        CreateMap<RegisterViewModel, ConfirmEmailViewModel>().ReverseMap();
        CreateMap<User, ConfirmEmailViewModel>().ReverseMap();
        CreateMap<Category, CategoryDb>().ReverseMap();
        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Post, PostDb>().ReverseMap();
        CreateMap<Post, PostForPostsViewModel>().ReverseMap();
    }
}