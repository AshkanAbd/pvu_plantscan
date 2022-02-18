using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PVU_PlantScan
{
    public class PlantContextFactory : IDesignTimeDbContextFactory<PlantContext>
    {
        public PlantContext CreateDbContext(string[] args)
        {
            Config.LoadConfiguration();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<PlantContext>()
                .UseNpgsql(Config.Configuration.GetConnectionString("DefaultConnection"));

            dbContextOptionsBuilder.EnableSensitiveDataLogging();


            return new PlantContext(dbContextOptionsBuilder.Options);
        }
    }
}