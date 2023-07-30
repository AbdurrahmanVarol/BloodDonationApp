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

    public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userService.GetByUsernameAsync(loginRequest.UserName) ?? throw new ArgumentException($"Kullanıcı adı ya da şifre hatalı");

        if (!VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new ArgumentException("Kullanıcı adı ya da şifre hatalı");
        }

        return _mapper.Map<UserResponse>(user);

    }
}
