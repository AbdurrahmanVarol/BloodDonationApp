using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using FluentValidation;

namespace BloodDonationApp.Business.Validators.FluentValidation;
public class HospitalValidator : AbstractValidator<Hospital>
{
    private readonly ICityRepository _repository;
    public HospitalValidator(ICityRepository repository)
    {
        _repository = repository;

        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Address).NotEmpty();
        RuleFor(p => p.CityId).NotEmpty().Must(IsCityExist);
        RuleFor(p => p.PhoneNumber).NotEmpty().Matches(@"\+90\(\d{3}\)\d{3}-\d{2}-\d{2}");
    }

    private bool IsCityExist(int cityId)
    {
        return _repository.IsExistAsync(p => p.Id == cityId).GetAwaiter().GetResult();
    }
}
