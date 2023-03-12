using Domain.Configration.EntitiesProperties;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Config.ConfigurationService;
using Presentation.Configration.Configrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsmsAPI.Test.Initialization
{
    class MyWebApplication : WebApplicationFactory<Program>
    {

        private string connectionString = "Server=.;Database=CSMS_Db;Trusted_Connection=True;";

        protected override IHost CreateHost(IHostBuilder builder)
        {

            builder.ConfigureServices(service =>
            {
                service.AddScopedService();
                service.AddScopedRepository();
                service.AddScopedAutoMapper();

                

                service.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                service.AddIdentity<Account, IdentityRole>(options =>
                {

                }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

                service.Configure<IdentityOptions>(options =>
                {
                    // Default Password settings.
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                });
            });

            return base.CreateHost(builder);
        }


    }
}
