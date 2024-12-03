using AutoMapper;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Database.Responses;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Domain.Validation.Validators;
using WheelDeal.Service.Interfaces;

namespace WheelDeal.Service.Realizations;

public class AutoParkService : IAutoParkService
{
    private readonly IBaseStorage<CategoryDb> _categoryStorage;

    private IMapper _mapper { get; set; }

    private CategoryValidator _validationRules { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public AutoParkService(IBaseStorage<CategoryDb> categoryStorage)
    {
        _categoryStorage = categoryStorage;
        _mapper = mapperConfiguration.CreateMapper();
        _validationRules = new CategoryValidator();
    }

    public BaseResponse<List<Category>> GetAllCategories()
    {
        try
        {
            var categoriesDb = _categoryStorage.GetAll().OrderBy(c => c.Name).ToList();
            var result = _mapper.Map<List<Category>>(categoriesDb);
            if (result.Count == 0)
                return new BaseResponse<List<Category>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };

            return new BaseResponse<List<Category>>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Category>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}