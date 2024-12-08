using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.Responses;

namespace WheelDeal.Service.Interfaces;

public interface IAutoParkService
{
    Task<BaseResponse<List<Category>>> GetAllCategories();
}