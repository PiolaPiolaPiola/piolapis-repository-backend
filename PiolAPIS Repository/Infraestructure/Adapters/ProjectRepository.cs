using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;
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
            var dbModel = new ProjectsDbModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Type = project.Type,
                Code = project.Code,
                IsActive = project.IsActive,
                CreatedDate = project.CreatedDate,
                UpdatedDate = project.UpdatedDate
            };

            _context.Projects.Add(dbModel);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Projects?> GetByIdAsync(Guid id)
        {
            var dbModel = await _context.Projects.FindAsync(id);
            if (dbModel == null) return null;

            return MapToDomain(dbModel);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Projects>> GetAllAsync()
        {
            var dbModels = await _context.Projects.ToListAsync();
            return dbModels.Select(MapToDomain);
        }

        public async Task<Projects> UpdateAsync(Projects project)
        {
            var dbModel = await _context.Projects.FindAsync(project.Id);
            if (dbModel == null)
                throw new KeyNotFoundException($"No se encontró el proyecto para actualizar con ID: {project.Id}");

            dbModel.Name = project.Name;
            dbModel.Description = project.Description;
            dbModel.Type = project.Type;
            dbModel.Code = project.Code;
            dbModel.IsActive = project.IsActive;
            dbModel.UpdatedDate = project.UpdatedDate;

            _context.Projects.Update(dbModel);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var dbModel = await _context.Projects.FindAsync(id);
            if (dbModel == null) return false;

            var domainProject = MapToDomain(dbModel);

            if (isActive)
                domainProject.Activate();
            else
                domainProject.Deactivate();

            dbModel.IsActive = domainProject.IsActive;
            dbModel.UpdatedDate = domainProject.UpdatedDate;

            _context.Projects.Update(dbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Delete(Guid id)
        {
            var dbModel = await _context.Projects.FindAsync(id);
            if (dbModel != null)
            {
                _context.Projects.Remove(dbModel);
                await _context.SaveChangesAsync();
            }
        }

        private static Projects MapToDomain(ProjectsDbModel dbModel)
        {
            return new Projects(
                id: dbModel.Id,
                name: dbModel.Name,
                description: dbModel.Description,
                type: dbModel.Type,
                code: dbModel.Code,
                isActive: dbModel.IsActive,
                createdDate: dbModel.CreatedDate,
                updatedDate: dbModel.UpdatedDate
            );
        }
    }
}