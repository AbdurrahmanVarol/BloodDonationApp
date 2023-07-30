using AutoMapper;
using BloodDonationApp.Business.Caching;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.DataAccess.Interfaces.Repositories;

namespace BloodDonationApp.Business.Services;
public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public CityService(ICityRepository cityRepository, IMapper mapper, ICache cache)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IEnumerable<CityResponse>> GetCitiesAsync()
    {
        var cachedCities = _cache.Get<IEnumerable<CityResponse>>("cities");
        if (cachedCities is null)
        {
            var cities = await _cityRepository.GetAllAsync();
            var mappedCities = _mapper.Map<IEnumerable<CityResponse>>(cities);
            _cache.Set("cities", mappedCities, TimeSpan.FromDays(1));
            cachedCities = mappedCities;
        }
        return cachedCities;
    }
}
