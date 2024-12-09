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
        CreateMap<PostDb, Post>()
            .ForMember(dest => dest.Stars, opt => opt.MapFrom(src =>
                src.Rates != null && src.Rates.Any()
                    ? (int)Math.Round(src.Rates.Average(r => r.Points), 0)
                    : 0))
            .ForMember(dest => dest.ImagesPaths, opt => opt.MapFrom(src => src.ImagesPaths))
            .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ReverseMap();

        CreateMap<CarDb, Car>().ReverseMap();
        CreateMap<Post, PostPageViewModel>().ReverseMap();
        CreateMap<PostFilter, PostForPostsViewModel>().ReverseMap();
        CreateMap<Post, PostForPostsViewModel>().ReverseMap();
    }
}