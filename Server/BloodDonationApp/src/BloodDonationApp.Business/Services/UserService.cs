using AutoMapper;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Extensions;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using BloodDonationApp.Entities.Enums;
using FluentValidation;

namespace BloodDonationApp.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<User> _validator;

    public UserService(IUserRepository userRepository, IMapper mapper, IValidator<User> validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task AddAsync(User user)
    {
        _validator.ValidateAndThrowValidationException(user);

        await _userRepository.AddAsync(user);
    }

    public async Task<IEnumerable<EmployeeResponse>> GetByHospitalIdAsync(Guid hospitalId)
    {
        var users = await _userRepository.GetAllAsync(p => p.HospitalId == hospitalId);

        //TODO:Mapping e EmloyeeResponse ekle
        return _mapper.Map<IEnumerable<EmployeeResponse>>(users);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetAsync(p => p.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _userRepository.GetAsync(p => p.UserName.Equals(username));
    }

    public async Task<IEnumerable<EmployeeResponse>> GetUnEmployedUsers()
    {
        var users = await _userRepository.GetAllAsync(p => p.RoleId == (int)Roles.Donor);

        //TODO:Mapping e EmloyeeResponse ekle
        return _mapper.Map<IEnumerable<EmployeeResponse>>(users);
    }

    public async Task<EmployeeManagementResponse> GetUsersForEmployeeManagement(Guid hospitalId)
    {
        var employees = await GetByHospitalIdAsync(hospitalId);
        var unEmployees = await GetUnEmployedUsers();
        var result = new EmployeeManagementResponse
        {
            Employees = employees,
            UnEmployedUsers = unEmployees
        };
        return result;
    }

    public async Task UpdateAsync(User user)
    {
        var hasUser = await _userRepository.IsExistAsync(p => p.Id == user.Id);
        if (!hasUser)
        {
            throw new ArgumentException(nameof(user));
        }

        _validator.ValidateAndThrowValidationException(user);

        await _userRepository.UpdateAsync(user);
    }
}
