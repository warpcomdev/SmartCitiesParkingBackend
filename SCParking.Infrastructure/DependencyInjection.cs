using SCParking.Domain.Interfaces;
using SCParking.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using SCParking.Core.Interfaces;
using SCParking.Core.Services;
using SCParking.Domain.Common;
using SCParking.Infrastructure.SmartCitiesAccess;

namespace SCParking.Infrastructure
{
    public class DependencyInjection
    {

        public static void RegisterServices(IServiceCollection services)
        {
            //Services           
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IRateDetailsService, RateDetailsService>();
            services.AddScoped<IParkingService, ParkingService>();
            services.AddScoped<IParkingReservationService, ParkingReservationService>();
            

            //Repositories           
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHelpers, Helpers>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRateDetailsRepository, RateDetailsRepository>();
            services.AddScoped<ISmartCityRepository, SmartCityRepository>();
            services.AddScoped<IParkingRepository, ParkingRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IParkingReservationRepository, ParkingReservationRepository>();

        }
    }
}
