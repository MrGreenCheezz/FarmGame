using FarmAppWebServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmAppWebServer.Contexts
{
    public class FarmAppDbContext : DbContext
    {
        public FarmAppDbContext(DbContextOptions<FarmAppDbContext> options) : base(options) { }
        public DbSet<UserRegistrationInfo> UserRegistrationInfos { get; set; }
        public DbSet<PlantTypesData> PlantTypesDataValues { get; set; }
        public DbSet<PlayerData> PlayerDataValues { get; set; }
        public DbSet<PlayersPlantsInstance> PlayersPlantsInstanceValues { get; set; }
    }
}
