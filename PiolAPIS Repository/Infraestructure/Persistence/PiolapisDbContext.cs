using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Infraestructure.Persistence
{
    public class PiolapisDbContext : DbContext
    {
        public PiolapisDbContext(DbContextOptions<PiolapisDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Documentations> Documentations { get; set; }
        public DbSet<Variables> Variables { get; set; }
        public DbSet<DocumentationSettings> DocumentationSettings { get; set; }
        public DbSet<CodeMessages> CodeMessages { get; set; }
        public DbSet<TemplatesDTOs> TemplatesDTOs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
