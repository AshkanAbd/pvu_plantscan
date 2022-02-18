using Microsoft.EntityFrameworkCore;

namespace PVU_PlantScan
{
    public class PlantContext : DbContext
    {
        protected PlantContext()
        {
        }

        public PlantContext(DbContextOptions<PlantContext> options) : base(options)
        {
        }

        public DbSet<PlantTableModel> Plants { get; set; }
    }
}