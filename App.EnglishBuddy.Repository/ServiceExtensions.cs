using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Infrastructure.Context;
using App.EnglishBuddy.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace App.EnglishBuddy.Infrastructure;
public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<EnglishBuddyDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICallsRepository, CallsRepository>();
        services.AddScoped<IOTPRepository, OTPRepository>();
        services.AddScoped<ICallInfoRepository, CallsInfoRepository>();
        services.AddScoped<IRatingsRepository, RatingsRepository>();
        services.AddScoped<ITotalRatingsRepository, TotalRatingsRepository>();
        services.AddScoped<IRandomUsersRepository, RandomUsersRepository>();
        services.AddScoped<IMeetingsRepository, MeetingsRepository>();
        services.AddScoped<IMeetingIdsRepository, MeetingIdsRepository>();
        services.AddScoped<IContactUsRepository, ContactUsRepository>();
        services.AddScoped<IUsersImagesRepository, UsersImagesRepository>();
        services.AddScoped<IMeetingsUsersRepository, MeetingsUsersRepository>();
    }
}