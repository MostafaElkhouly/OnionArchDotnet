using Persistence.IRepository;
using Persistence.IRepository.IEntityRepository;
using Persistence.IRepository.IUserRepository;
using Persistence.Repository;
using Persistence.Repository.EntityRepository;
using Persistence.Repository.UserRepository;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Executed.IExecuteies;
using Infrastructure.Executed.Executeies;

namespace Presentation.Config.ConfigurationService
{
    public static class ScopedRepositoryConfiguration
    {
        public static IServiceCollection AddScopedRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddTransient(typeof(IServiceOrchestrator), typeof(ServiceOrchestrator));
            

            return services;
        }
    }
}
