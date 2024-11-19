using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Database.Responses;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; }
    
    public StatusCode StatusCode { get; set; }
    
    public T Data { get; set; }
}

public interface IBaseResponse<T>
{
    T Data { get; set; }
}

public enum StatusCode
{
    OK = 200,
    BadRequest = 400,
    NotFound = 404,
    InternalServerError = 500,
}