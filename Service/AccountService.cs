using WheelDeal.Interfaces;

namespace WheelDeal.Service;

public class AccountService : IAccountService
{
    private readonly IBaseStorage<UserDb> _userStorage;
    
    private IMapper _mapper;
}