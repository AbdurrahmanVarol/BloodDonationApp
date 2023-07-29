using AutoMapper;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.DataAccess.Interfaces.Repositories;

namespace BloodDonationApp.Business.Services;
public class GenderService : IGenderService
{
    private readonly IGenderRepository _genderRepository;
    private readonly IMapper _mapper;

    public GenderService(IGenderRepository genderRepository, IMapper mapper)
    {
        _genderRepository = genderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenderResponse>> GetGendersAsync()
    {
        var genders = await _genderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GenderResponse>>(genders);
    }
}
