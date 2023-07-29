using AutoMapper;
using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Entities.Entities;
using BloodDonationApp.Entities.Enums;
using System.Text;

namespace BloodDonationApp.Business.Services;
public class AuthServiceBase : IAuthService
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthServiceBase(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = Convert.ToBase64String(hmac.Key);
        passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(passwordSalt));

        var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

        return passwordHash.Equals(computedHash);
    }

    public async Task RegisterAsync(RegisterRequest registerRequest)
    {
        CreatePasswordHash(registerRequest.Password, out string passwordHash, out string passwordSalt);

        var user = _mapper.Map<User>(registerRequest);
        user.PasswordSalt = passwordSalt;
        user.PasswordHash = passwordHash;
        user.RoleId = (int)Roles.Donor;
        await _userService.AddAsync(user);
    }
}
