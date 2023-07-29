using AutoMapper;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.DataAccess.Interfaces.Repositories;

namespace BloodDonationApp.Business.Services;
public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public CityService(ICityRepository cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CityResponse>> GetCitiesAsync()
    {
        var cities = await _cityRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CityResponse>>(cities);
    }
}
