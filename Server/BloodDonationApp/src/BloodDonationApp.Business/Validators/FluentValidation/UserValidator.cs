using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using FluentValidation;

namespace BloodDonationApp.Business.Validators.FluentValidation;
public class UserValidator : AbstractValidator<User>
{
    private readonly ICityRepository _cityRepository;
    private readonly IHospitalRepository _hospitalRepository;
    private readonly IBloodGroupRepository _bloodGroupRepository;
    public UserValidator(ICityRepository cityRepository, IHospitalRepository hospitalRepository, IBloodGroupRepository bloodGroupRepository)
    {
        _cityRepository = cityRepository;
        _hospitalRepository = hospitalRepository;
        _bloodGroupRepository = bloodGroupRepository;

        RuleFor(p => p.FirstName).NotEmpty();
        RuleFor(p => p.LastName).NotEmpty();
        RuleFor(p => p.Email).NotEmpty().EmailAddress();
        RuleFor(p => p.UserName).NotEmpty();
        RuleFor(p => p.PasswordHash).NotEmpty();
        RuleFor(p => p.PasswordSalt).NotEmpty();
        RuleFor(p => p.CityId).NotEmpty().Must(IsCityExist);
        RuleFor(p => p.BloodGroupId).NotEmpty().Must(IsBloodGroupExist);
        When(p => p.HospitalId != null, () =>
        {
            RuleFor(p => p.HospitalId).NotEmpty().Must(IsHospitalExist);
        });

    }
    private bool IsCityExist(int cityId)
    {
        return _cityRepository.IsExistAsync(p => p.Id == cityId).GetAwaiter().GetResult();
    }
    private bool IsBloodGroupExist(int bloodGroupId)
    {
        return _bloodGroupRepository.IsExistAsync(p => p.Id == bloodGroupId).GetAwaiter().GetResult();
    }
    private bool IsHospitalExist(Guid? hospitalId)
    {
        return _hospitalRepository.IsExistAsync(p => p.Id == hospitalId).GetAwaiter().GetResult();
    }
}
