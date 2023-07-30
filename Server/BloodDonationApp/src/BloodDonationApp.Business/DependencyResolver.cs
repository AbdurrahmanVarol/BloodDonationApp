using BloodDonationApp.Business.Caching;
using BloodDonationApp.Business.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BloodDonationApp.Business
{
    public static class DependencyResolver
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IMvcAuthService, MvcAuthService>();
            services.AddScoped<IApiAuthService, ApiAuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHospitalService, HospitalService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IBloodGroupService, BloodGroupSerivce>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<ICache, InMemoryCache>();

            services.AddMemoryCache();

        }
    }
}
