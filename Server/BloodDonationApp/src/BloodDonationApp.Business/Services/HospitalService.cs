using AutoMapper;
using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Extensions;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using BloodDonationApp.Entities.Enums;
using FluentValidation;

namespace BloodDonationApp.Business.Services;

public class HospitalService : IHospitalService
{
    private readonly IHospitalRepository _hospitalRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<Hospital> _validator;
    private readonly IUserService _userService;

    public HospitalService(IHospitalRepository hospitalRepository, IMapper mapper, IValidator<Hospital> validator, IUserService userService)
    {
        _hospitalRepository = hospitalRepository;
        _mapper = mapper;
        _validator = validator;
        _userService = userService;
    }

    public async Task<Guid> AddAsync(CreateHospitalRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var hospital = _mapper.Map<Hospital>(request);

        _validator.ValidateAndThrowValidationException(hospital);

        await _hospitalRepository.AddAsync(hospital);

        return hospital.Id;
    }

    public async Task AddEmployeeAsync(AddEmployeeRequest request)
    {
        var employee = await _userService.GetByIdAsync(request.EmployeeId) ?? throw new ArgumentNullException($"{request.EmployeeId} Id'li kullanıcı bulunamadı.");

        employee.HospitalId = request.HospitalId;
        employee.RoleId = (int)Roles.Staff;

        await _userService.UpdateAsync(employee);
    }

    public async Task RemoveEmployeeAsync(RemoveEmployeeRequest request)
    {

        var employee = await _userService.GetByIdAsync(request.EmployeeId) ?? throw new ArgumentNullException($"{request.EmployeeId} Id'li personel bulunamadı.");

        employee.HospitalId = null;
        employee.RoleId = (int)Roles.Donor;

        await _userService.UpdateAsync(employee);
    }

    public async Task<HospitalDisplayResponse?> GetByIdAsync(Guid id)
    {
        var hospital = await _hospitalRepository.GetAsync(p => p.Id == id);

        return _mapper.Map<HospitalDisplayResponse?>(hospital);
    }

    public async Task<IEnumerable<HospitalDisplayResponse>> GetHospitalsAsync()
    {
        var hospitals = await _hospitalRepository.GetHospitalsWithIncludes();

        return _mapper.Map<IEnumerable<HospitalDisplayResponse>>(hospitals);
    }

    public async Task<HospitalUpdateResponse?> GetHospitalForUpdateByIdAsync(Guid id)
    {
        var hospital = await _hospitalRepository.GetAsync(p => p.Id == id);

        return _mapper.Map<HospitalUpdateResponse?>(hospital);
    }

    public async Task UpdateAsync(UpdateHospitalRequest updateHospitalRequest)
    {
        if (updateHospitalRequest is null)
        {
            throw new ArgumentNullException(nameof(updateHospitalRequest));
        }

        var hospital = await _hospitalRepository.GetAsync(p => p.Id == updateHospitalRequest.Id) ?? throw new ArgumentException($"{updateHospitalRequest.Id} Id'li hastane bulunamadı.");

        hospital.CityId = updateHospitalRequest.CityId;
        hospital.PhoneNumber = updateHospitalRequest.PhoneNumber;
        hospital.Address = updateHospitalRequest.Address;
        hospital.Name = updateHospitalRequest.Name;

        _validator.ValidateAndThrowValidationException(hospital);

        await _hospitalRepository.UpdateAsync(hospital);
    }

    public async Task DeleteAsync(Guid id)
    {
        var hospital = await _hospitalRepository.GetAsync(p => p.Id == id) ?? throw new ArgumentException($"{id} Id'li hastane bulunamadı.");

        await _hospitalRepository.DeleteAsync(hospital);

    }

    public async Task<bool> HasHospitalAsync(Guid id)
    {
        return await _hospitalRepository.IsExistAsync(p => p.Id == id);
    }
}
