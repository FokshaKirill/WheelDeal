using AutoMapper;
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
    
    public IMapper _mapper { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public PostService(IBaseStorage<PostDb> postStorage)
    {
        _postStorage = postStorage;
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    public BaseResponse<List<Post>> GetAllPostsByIdCategory(Guid id)
    {
        try
        {
            var postsDb = _postStorage.GetAll()
                .Where(post => post.CategoryId.Equals(id)) // Сравниваем с CategoryId
                .OrderBy(p => p.CreatedAt)
                .ToList();
            var result = _mapper.Map<List<Post>>(postsDb);
            if (result.Count == 0)
                return new BaseResponse<List<Post>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };

            return new BaseResponse<List<Post>>()
            {
                Data = result,
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
}