using AutoMapper;
using BloodDonationApp.Business.Caching;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.DataAccess.Interfaces.Repositories;

namespace BloodDonationApp.Business.Services;
public class BloodGroupSerivce : IBloodGroupService
{
    private readonly IBloodGroupRepository _bloodGroupRepository;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public BloodGroupSerivce(IBloodGroupRepository bloodGroupRepository, IMapper mapper, ICache cache)
    {
        _bloodGroupRepository = bloodGroupRepository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IEnumerable<BloodGroupDisplayResponse>> GetBloodGroupsAsync()
    {
        var cachedBloodGroups = _cache.Get<IEnumerable<BloodGroupDisplayResponse>>("bloodgroups");
        if (cachedBloodGroups is null)
        {
            var bloodGroups = await _bloodGroupRepository.GetAllAsync();
            var mappedBloodGroup = _mapper.Map<IEnumerable<BloodGroupDisplayResponse>>(bloodGroups);
            _cache.Set("bloodgroups", mappedBloodGroup, TimeSpan.FromDays(1));
            cachedBloodGroups = mappedBloodGroup;
        }
        return cachedBloodGroups;
    }
}
