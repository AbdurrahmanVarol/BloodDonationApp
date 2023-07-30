using AutoMapper;
using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Extensions;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using BloodDonationApp.Entities.Enums;
using FluentValidation;

namespace BloodDonationApp.Business.Services;
public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IValidator<Request> _validator;

    public RequestService(IRequestRepository requestRepository, IUserService userService, IMapper mapper, IValidator<Request> validator)
    {
        _requestRepository = requestRepository;
        _userService = userService;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Guid> AddAsync(CreateRequestRequest createRequestRequest)
    {

        Guid hospitalId = default;


        if (createRequestRequest.HospitalId == null || createRequestRequest.HospitalId == default)
        {
            var user = await _userService.GetByIdAsync(createRequestRequest.UserId) ?? throw new ArgumentException($"{createRequestRequest.UserId} Id'li kullanıcı bulunamadı");
            hospitalId = user.HospitalId ?? default;
        }
        else
        {
            hospitalId = (Guid)createRequestRequest.HospitalId;
        }

        var addedRequest = await _requestRepository.GetAsync(p => p.BloodGroupId == createRequestRequest.BloodGroupId && p.HospitalId == hospitalId);

        if (addedRequest is not null)
        {
            var updateRequest = new UpdateRequestRequest
            {
                Id = addedRequest.Id,
                BloodGroupId = createRequestRequest.BloodGroupId,
                HospitalId = hospitalId,
                Quantity = createRequestRequest.Quantity + addedRequest.Quantity,
            };
            await UpdateAsync(updateRequest);
            return addedRequest.Id;
        }

        var request = _mapper.Map<Request>(createRequestRequest);

        request.HospitalId = hospitalId;

        _validator.ValidateAndThrowValidationException(request);

        await _requestRepository.AddAsync(request);

        return request.Id;
    }

    public async Task<RequestDisplayResponse?> GetByIdAsync(Guid id)
    {
        var request = await _requestRepository.GetAsync(p => p.Id == id) ?? throw new ArgumentException($"{id} Id'li talep bulunamadı");

        return _mapper.Map<RequestDisplayResponse>(request);
    }

    public async Task<RequestUpdateResponse> GetRequestForUpdateByIdAsync(Guid id)
    {
        var request = await _requestRepository.GetAsync(p => p.Id == id) ?? throw new ArgumentException($"{id} Id'li talep bulunamadı");

        return _mapper.Map<RequestUpdateResponse>(request);
    }

    public async Task DeleteAsync(Guid id)
    {
        var request = await _requestRepository.GetAsync(p => p.Id == id) ?? throw new ArgumentException($"{id} Id'li talep bulunamadı.");
        await _requestRepository.DeleteAsync(request);
    }

    public async Task UpdateAsync(UpdateRequestRequest updateRequestRequest)
    {
        var request = await _requestRepository.GetAsync(p => p.Id == updateRequestRequest.Id) ?? throw new ArgumentException($"{updateRequestRequest.Id} Id'li talep bulunamadı.");

        if (request.Quantity == 0)
        {
            await DeleteAsync(updateRequestRequest.Id);
            return;
        }

        request.Quantity = updateRequestRequest.Quantity;
        request.BloodGroupId = updateRequestRequest.BloodGroupId;
        request.HospitalId = updateRequestRequest.HospitalId;

        _validator.ValidateAndThrowValidationException(request);

        await _requestRepository.UpdateAsync(request);
    }

    public async Task<IEnumerable<RequestDisplayResponse>> GetRequestsByUserIdAsync(Guid userId)
    {
        //TODO: Talepler kangrubuna göre listelensin
        //TODO:Refactor
        var user = await _userService.GetByIdAsync(userId) ?? throw new ArgumentException($"{userId} Id'li kullanıcı bulunamadı");

        if (user.RoleId == (int)Roles.Donor)
        {
            return Enumerable.Empty<RequestDisplayResponse>();
        }
        else if (user.RoleId == (int)Roles.Admin)
        {

            var requests = await _requestRepository.GetRequestsWithIncludes();
            return _mapper.Map<IEnumerable<RequestDisplayResponse>>(requests);
        }
        else
        {
            var requests = await _requestRepository.GetRequestsWithIncludes(p => p.HospitalId == user.HospitalId);
            return _mapper.Map<IEnumerable<RequestDisplayResponse>>(requests);
        }
    }

    public async Task<IEnumerable<RequestDisplayResponse>> GetRequestsByBloodGroupIdAsync(int bloodGroupId)
    {

        var requests = await _requestRepository.GetRequestsWithIncludes(p => p.BloodGroupId == bloodGroupId);
        return _mapper.Map<IEnumerable<RequestDisplayResponse>>(requests);
    }

    public async Task<IEnumerable<RequestDisplayResponse>> GetRequestsAsync()
    {

        var requests = await _requestRepository.GetRequestsWithIncludes();
        return _mapper.Map<IEnumerable<RequestDisplayResponse>>(requests);
    }
}
