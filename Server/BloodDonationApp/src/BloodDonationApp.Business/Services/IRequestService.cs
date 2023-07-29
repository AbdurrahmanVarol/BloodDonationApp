using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.Business.Services;
public interface IRequestService
{
    Task<Guid> AddAsync(CreateRequestRequest createRequestRequest);
    Task DeleteAsync(Guid id);
    Task<RequestUpdateResponse> GetRequestForUpdateByIdAsync(Guid id);
    Task<IEnumerable<RequestDisplayResponse>> GetRequests();
    Task<IEnumerable<RequestDisplayResponse>> GetRequestsByBloodGroupId(int cityId);
    Task<IEnumerable<RequestDisplayResponse>> GetRequestsByUserId(Guid userId);
    Task UpdateAsync(UpdateRequestRequest updateRequestRequest);
}
