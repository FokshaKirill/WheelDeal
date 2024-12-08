using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WheelDeal.Domain.Database;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Database.Responses;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Service.Interfaces;

namespace WheelDeal.Service.Realizations;

public class PostService : IPostService
{
    private readonly IBaseStorage<PostDb> _postStorage;
    private readonly IMapper _mapper;

    public PostService(IBaseStorage<PostDb> postStorage, IMapper mapper)
    {
        _postStorage = postStorage;
        _mapper = mapper;
    }

    public BaseResponse<List<Post>> GetAllPostsByIdCategory(Guid id)
    {
        try
        {
            var postsDb = _postStorage.GetAll()
                .Where(post => post.CategoryId == id)
                .Include(p => p.Car) // Загрузка связанных данных
                .Include(p => p.Category) // Загрузка категории
                .Include(p => p.Rates) // Загрузка оценок
                .OrderBy(p => p.CreatedAt)
                .ToList();

            var posts = _mapper.Map<List<Post>>(postsDb);

            return new BaseResponse<List<Post>>()
            {
                Data = posts,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Post>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<Post>> GetPostById(Guid id)
    {
        try
        {
            var postDb = await _postStorage.Get(id);

            var result = _mapper.Map<Post>(postDb);

            if (result == null)
            {
                return new BaseResponse<Post>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }
            
            return new BaseResponse<Post>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Post>()
            {
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
