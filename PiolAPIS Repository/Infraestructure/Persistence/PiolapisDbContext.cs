using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;

namespace PiolAPIS_Repository.Infraestructure.Persistence
{
    public class PiolapisDbContext : DbContext
    {
        public DbSet<UsersDbModel> Users { get; set; }
        public DbSet<CodeMessagesDbModel> CodeMessages { get; set; }
        public DbSet<DocumentationsDbModel> Documentations { get; set; }
        public DbSet<DocumentationSettingsDbModel> DocumentationSettings { get; set; }
        public DbSet<ProjectsDbModel> Projects { get; set; }
        public DbSet<TemplatesDTOsDbModel> TemplatesDTOs { get; set; }
        public DbSet<VariablesDbModel> Variables { get; set; }

        public PiolapisDbContext(DbContextOptions<PiolapisDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (!string.IsNullOrEmpty(tableName))
                {
                    entity.SetTableName(tableName.ToLowerInvariant());
                }
            }
        }
    }
}
