using AutoMapper;
using BloodDonationApp.Business.Caching;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.DataAccess.Interfaces.Repositories;

namespace BloodDonationApp.Business.Services;
public class GenderService : IGenderService
{
    private readonly IGenderRepository _genderRepository;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GenderService(IGenderRepository genderRepository, IMapper mapper, ICache cache)
    {
        _genderRepository = genderRepository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IEnumerable<GenderResponse>> GetGendersAsync()
    {
        var cachedGenders = _cache.Get<IEnumerable<GenderResponse>>("genders");
        if (cachedGenders is null)
        {
            var genders = await _genderRepository.GetAllAsync();
            var mappetGenders = _mapper.Map<IEnumerable<GenderResponse>>(genders);
            _cache.Set("genders", mappetGenders, TimeSpan.FromDays(1));
            cachedGenders = mappetGenders;
        }
        return cachedGenders;
    }
}
