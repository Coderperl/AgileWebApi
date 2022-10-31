using Microsoft.EntityFrameworkCore;

namespace AgileWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Elevator> Elevators { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
