using AutoMapper;
using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Entities.Entities;

namespace BloodDonationApp.Business.Mapping.AutoMapper;

public class Defaultmapper : Profile
{
    public Defaultmapper()
    {
        CreateMap<User, UserResponse>();
        CreateMap<User, EmployeeResponse>()
            .ForMember(s => s.City, d => d.MapFrom(m => ""));
        //TODO:City i ekle

        CreateMap<RegisterRequest, User>()
            .ForMember(p => p.PasswordHash, d => d.MapFrom(m => string.Empty))
            .ForMember(p => p.PasswordSalt, d => d.MapFrom(m => string.Empty));

        CreateMap<Hospital, HospitalUpdateResponse>();
        CreateMap<Hospital, HospitalDisplayResponse>()
            .ForMember(s => s.City, d => d.MapFrom(m => m.City == null ? "" : $"{m.City.Name}"));

        CreateMap<City, CityResponse>();

        CreateMap<CreateHospitalRequest, Hospital>();

        CreateMap<CreateRequestRequest, Request>();
        CreateMap<Request, RequestDisplayResponse>()
            .ForMember(s => s.BloodGroup, d => d.MapFrom(m => $"{m.BloodGroup.Name}({m.BloodGroup.Symbol})"))
            .ForMember(s => s.Hospital, d => d.MapFrom(m => $"{m.Hospital.Name}"))
            .ForMember(s => s.City, d => d.MapFrom(m => $"{m.Hospital.City.Name}"));
        CreateMap<Request, RequestUpdateResponse>();

        CreateMap<BloodGroup, BloodGroupDisplayResponse>();

        CreateMap<Gender, GenderResponse>();
    }
}
