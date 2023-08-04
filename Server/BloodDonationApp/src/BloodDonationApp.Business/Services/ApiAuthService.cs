using AutoMapper;
using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Entities.Entities;
using BloodDonationApp.Entities.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BloodDonationApp.Business.Services;
public class ApiAuthService : AuthServiceBase, IApiAuthService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    public ApiAuthService(IUserService userService, IMapper mapper, IConfiguration configuration) : base(userService, mapper)
    {
        _userService = userService;
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var key = _configuration.GetSection("JWT:Key").Value;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creadentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.FirstName),
            new Claim(ClaimTypes.Surname,user.LastName),
            new Claim("BloodGroup",user.BloodGroupId.ToString()),
            new Claim("City",user.CityId.ToString()),
            new Claim(ClaimTypes.Role,((Roles)user.RoleId).ToString())
        };
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: "",
            audience: "",
            claims: claims,
            expires: DateTime.Now.AddDays(5),
            notBefore: DateTime.Now,
            signingCredentials: creadentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        if (loginRequest is null)
        {
            throw new ArgumentNullException(nameof(loginRequest));
        }
        var user = await _userService.GetByUsernameAsync(loginRequest.UserName) ?? throw new ArgumentException($"Kullanıcı adı ya da şifre hatalı");

        if (!VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new ArgumentException("Kullanıcı adı ya da şifre hatalı");
        }

        var token = GenerateToken(user);
        var response = new LoginResponse
        {
            Token = token,
            UserName = $"{user.FirstName} {user.LastName}",
            Expire = DateTime.Now.AddDays(5),
            BloodGroup = user.BloodGroupId,
            City = user.CityId,
            UserRole = user.RoleId,
        };
        return response;
    }
}
