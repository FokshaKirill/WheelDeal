using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.Responses;

namespace WheelDeal.Service.Interfaces;

public interface IAutoParkService
{
    BaseResponse<List<Category>> GetAllCategories();
}