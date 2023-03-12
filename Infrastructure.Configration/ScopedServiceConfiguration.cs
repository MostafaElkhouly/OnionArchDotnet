using Microsoft.Extensions.DependencyInjection;
using Service.Data;
using Service.Interface;

namespace Presentation.Config.ConfigurationService
{
    public static class ScopedServiceConfiguration
    {
        public static IServiceCollection AddScopedService(this IServiceCollection services)
        {

            services.AddTransient(typeof(ICarService), typeof(CarService));
            services.AddTransient(typeof(IColorService), typeof(ColorService));
            services.AddTransient(typeof(ICarModelService), typeof(CarModelService));
            services.AddTransient(typeof(IJobCardService), typeof(JobCardService));
            services.AddTransient(typeof(ISparPartService), typeof(SparPartService));
            services.AddTransient(typeof(ITicketService), typeof(TicketService));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IRoleService), typeof(RoleService));


            return services;
        }
    }
}
