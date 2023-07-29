using AutoMapper;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.DataAccess.Interfaces.Repositories;

namespace BloodDonationApp.Business.Services;
public class BloodGroupSerivce : IBloodGroupService
{
    private readonly IBloodGroupRepository _bloodGroupRepository;
    private readonly IMapper _mapper;

    public BloodGroupSerivce(IBloodGroupRepository bloodGroupRepository, IMapper mapper)
    {
        _bloodGroupRepository = bloodGroupRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BloodGroupDisplayResponse>> GetBloodGroupsAsync()
    {
        var bloodGroups = await _bloodGroupRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BloodGroupDisplayResponse>>(bloodGroups);
    }
}
