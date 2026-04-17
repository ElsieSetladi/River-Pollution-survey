using Microsoft.EntityFrameworkCore;

namespace River_Pollution_Survey.Models.DBModels
{
    public class RiverDBContext : DbContext
    {
        public RiverDBContext()
        {
        }

        public RiverDBContext(DbContextOptions<RiverDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Site> Sites { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Waste> Wastes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=SHENNY;Initial Catalog=RiverPollutionSurvey;Integrated Security=True;Encrypt=False");
            }
        }
    }
}
