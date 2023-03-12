

using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Configration.EntitiesProperties
{
    public partial class AppDbContext
    {
        public DbSet<Car> Car { get; set; }
        public DbSet<JobCard> JobCard { get; set; }
        public DbSet<JobCard_SparPart> JobCard_SparPart { get; set; }
        public DbSet<SparPart> SparPart { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<CarModel> CarModel { get; set; }
    }
}
