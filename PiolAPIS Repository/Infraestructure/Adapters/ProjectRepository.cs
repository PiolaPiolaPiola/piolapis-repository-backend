using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.Project;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PiolapisDbContext _context;

        public ProjectRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task<Projects> SaveAsync(Projects project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Projects?> GetByIdAsync(Guid id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            var project = await GetByIdAsync(id);
            return project != null;
        }

        public async Task<IEnumerable<Projects>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Projects> UpdateAsync(Projects project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            if (isActive)
                project.Activate();
            else
                project.Deactivate();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Delete(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
    }
}