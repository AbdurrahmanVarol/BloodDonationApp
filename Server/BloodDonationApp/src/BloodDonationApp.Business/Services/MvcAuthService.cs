using AutoMapper;
using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.Business.Services;

public class MvcAuthService : AuthServiceBase, IMvcAuthService
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public MvcAuthService(IUserService userService, IMapper mapper) : base(userService, mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public Task<UserResponse> LoginAsync(LoginRequest loginRequest)
    {
        throw new NotImplementedException();
    }
}
