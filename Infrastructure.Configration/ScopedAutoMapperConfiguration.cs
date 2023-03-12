


using AutoMapper;
using Infrastructure.ViewModel.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Configration.Configrations
{
    public static class ScopedAutoMapperConfiguration
    {
        public static IServiceCollection AddScopedAutoMapper(this IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(mapper =>
            {

                mapper.AddProfile(new UserProfile());
                mapper.AddProfile(new RoleProfile());
                mapper.AddProfile(new CarProfile());
                mapper.AddProfile(new JobCardProfile());
                mapper.AddProfile(new TicketProfile());
                mapper.AddProfile(new SparPartProfile());
                mapper.AddProfile(new ColorProfile());
                mapper.AddProfile(new CarModelProfile());

            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            return services;
        }
    }
}
