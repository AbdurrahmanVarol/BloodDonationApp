using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.Business.Services;

public interface IHospitalService
{
    Task<Guid> AddAsync(CreateHospitalRequest request);
    Task<HospitalDisplayResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<HospitalDisplayResponse>> GetHospitalsAsync();
    Task<HospitalUpdateResponse?> GetHospitalForUpdateByIdAsync(Guid id);
    Task AddEmployeeAsync(AddEmployeeRequest request);
    Task RemoveEmployeeAsync(RemoveEmployeeRequest request);
    Task UpdateAsync(UpdateHospitalRequest updateHospitalRequest);
    Task DeleteAsync(Guid id);
    Task<bool> HasHospitalAsync(Guid id);
}
