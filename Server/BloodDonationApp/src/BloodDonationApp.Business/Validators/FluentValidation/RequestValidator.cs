using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using FluentValidation;

namespace BloodDonationApp.Business.Validators.FluentValidation;
public class RequestValidator : AbstractValidator<Request>
{
    private readonly IBloodGroupRepository _bloodGroupRepository;
    private readonly IHospitalRepository _hospitalRepository;
    public RequestValidator(IBloodGroupRepository bloodGroupRepository, IHospitalRepository hospitalRepository)
    {
        _bloodGroupRepository = bloodGroupRepository;
        _hospitalRepository = hospitalRepository;
        RuleFor(p => p.Quantity).NotEmpty().GreaterThan(0);
        RuleFor(p => p.BloodGroupId).NotEmpty().Must(IsBloodGroupExist);
        RuleFor(p => p.HospitalId).NotEmpty().Must(IsHospitalExist);

    }
    private bool IsHospitalExist(Guid hospitalId)
    {
        return _hospitalRepository.IsExistAsync(p => p.Id == hospitalId).GetAwaiter().GetResult();
    }
    private bool IsBloodGroupExist(int bloodGroupId)
    {
        return _bloodGroupRepository.IsExistAsync(p => p.Id == bloodGroupId).GetAwaiter().GetResult();
    }
}
