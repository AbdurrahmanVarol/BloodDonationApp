﻿using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Entities.Entities;

namespace BloodDonationApp.Business.Services;

public interface IUserService
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User employee);
    Task<IEnumerable<EmployeeResponse>> GetByHospitalIdAsync(Guid hospitalId);
    Task<IEnumerable<EmployeeResponse>> GetUnEmployedUsersAsync();
    Task<EmployeeManagementResponse> GetUsersForEmployeeManagementAsync(Guid hospitalId);
    Task<IEnumerable<User>> GetUsersByHospitalIdAsync(Guid hospitalId);
}
