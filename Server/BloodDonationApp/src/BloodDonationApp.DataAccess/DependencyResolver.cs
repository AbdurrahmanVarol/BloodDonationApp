using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.DataAccess.Entityframework.Repositories;
using BloodDonationApp.DataAccess.Entityframework.Transaction;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.DataAccess.Interfaces.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonationApp.DataAccess;

public static class DependencyResolver
{
    public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServerConnectionString");

        services.AddDbContext<BloodDonationAppContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IBloodGroupRepository, EfBloodGroupRepository>();
        services.AddScoped<IGenderRepository, EfGenderRepository>();
        services.AddScoped<IHospitalRepository, EfHospitalRepository>();
        services.AddScoped<IRequestRepository,EfRequestRepository>();
        services.AddScoped<ICityRepository, EfCityRepository>();

        services.AddScoped<IDatabaseTransaction, EfDatabaseTransaction>();
    }
}
