using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using FluentValidation;

namespace BloodDonationApp.Business.Validators.FluentValidation;
public class UserValidator : AbstractValidator<User>
{
    private readonly ICityRepository _cityRepository;
    private readonly IHospitalRepository _hospitalRepository;
    private readonly IBloodGroupRepository _bloodGroupRepository;
    private readonly IGenderRepository _genderRepository;
    private readonly IUserRepository _userRepository;
    public UserValidator(ICityRepository cityRepository, IHospitalRepository hospitalRepository, IBloodGroupRepository bloodGroupRepository, IGenderRepository genderRepository, IUserRepository userRepository)
    {
        _cityRepository = cityRepository;
        _hospitalRepository = hospitalRepository;
        _bloodGroupRepository = bloodGroupRepository;
        _genderRepository = genderRepository;
        _userRepository = userRepository;

        RuleFor(p => p.FirstName).NotEmpty();
        RuleFor(p => p.LastName).NotEmpty();
        RuleFor(p => p.Email).NotEmpty().EmailAddress();
        RuleFor(p => p.UserName).NotEmpty();
        RuleFor(p => p.PasswordHash).NotEmpty();
        RuleFor(p => p.PasswordSalt).NotEmpty();
        RuleFor(p => p.CityId).NotEmpty().Must(IsCityExist);
        RuleFor(p => p.GenderId).NotEmpty().Must(IsGenderExist);
        RuleFor(p => p.BloodGroupId).NotEmpty().Must(IsBloodGroupExist);
        When(p => p.HospitalId != null, () =>
        {
            RuleFor(p => p.HospitalId).NotEmpty().Must(IsHospitalExist);
        });
        When(p => p.Id == default, () =>
        {
            RuleFor(p => p.UserName).Must(p => !IsUserNameExist(p)).WithMessage(p => $"{p.UserName} kullanıcı adı kullanılmaktadır");
        }).Otherwise(() =>
        {
            RuleFor(p => p.UserName).Must((p, context) => IsUserNameAvailableForUpdate(p.Id, p.UserName)).WithMessage(p => $"{p} kullanıcı adı kullanılmaktadır");
        });
    }

    private bool IsUserNameAvailableForUpdate(Guid id, string userName)
    {
        var existingUser = _userRepository.GetAsync(p => p.UserName.Equals(userName)).GetAwaiter().GetResult();

        return existingUser == null || existingUser.Id == id;
    }

    private bool IsUserNameExist(string userName)
    {
        return _userRepository.IsExistAsync(p => p.UserName.Equals(userName)).GetAwaiter().GetResult();
    }
    private bool IsCityExist(int cityId)
    {
        return _cityRepository.IsExistAsync(p => p.Id == cityId).GetAwaiter().GetResult();
    }
    private bool IsGenderExist(int genderId)
    {
        return _genderRepository.IsExistAsync(p => p.Id == genderId).GetAwaiter().GetResult();
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
