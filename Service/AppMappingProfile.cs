using AutoMapper;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.ViewModels.LogAndReg;

namespace WheelDeal.Service;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDb>().ReverseMap();
        CreateMap<User, LoginViewModel>().ReverseMap();
        CreateMap<User, RegisterViewModel>().ReverseMap();
        CreateMap<RegisterViewModel, ConfirmEmailViewModel>().ReverseMap();
        CreateMap<User, ConfirmEmailViewModel>().ReverseMap();
    }
}
