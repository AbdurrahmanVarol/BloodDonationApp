using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.Business.Services;
public interface IRequestService
{
    Task<Guid> AddAsync(CreateRequestRequest createRequestRequest);
    Task DeleteAsync(Guid id);
    Task<RequestDisplayResponse?> GetByIdAsync(Guid id);
    Task<RequestUpdateResponse> GetRequestForUpdateByIdAsync(Guid id);
    Task<IEnumerable<RequestDisplayResponse>> GetRequestsAsync();
    Task<IEnumerable<RequestDisplayResponse>> GetRequestsByBloodGroupIdAsync(int cityId);
    Task<IEnumerable<RequestDisplayResponse>> GetRequestsByUserIdAsync(Guid userId);
    Task UpdateAsync(UpdateRequestRequest updateRequestRequest);
}
