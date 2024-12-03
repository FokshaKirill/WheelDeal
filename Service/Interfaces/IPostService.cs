using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.Responses;

namespace WheelDeal.Service.Interfaces;

public interface IPostService
{
    BaseResponse<List<Post>> GetAllPostsByNameCategory(Guid id);
}