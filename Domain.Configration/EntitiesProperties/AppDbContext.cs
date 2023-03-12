using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace Domain.Configration.EntitiesProperties
{
    public partial class AppDbContext : IdentityDbContext<Account>
    {

        //private readonly string StringConnection = "Server=.;Database=CSMS_Db;Trusted_Connection=True;";
        private readonly IConfiguration config;

        public AppDbContext(DbContextOptions<AppDbContext> options,
            IConfiguration config) : base(options)
        {
            this.config = config;
        }

        public AppDbContext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddAppDbProperties();


        }
    }
}
