using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.Documentation;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class DocumentationRepository : IDocumentationRepository
    {
        private readonly PiolapisDbContext _context;

        public DocumentationRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Documentations documentation)
        {
            _context.Documentations.Add(documentation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Documentations documentation)
        {
            _context.Update(documentation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var documentation = await _context.Documentations.FindAsync(id);
            if (documentation == null) return false;

            if (isActive)
                documentation.Activate();
            else
                documentation.Deactivate();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Documentations?> GetByIdAsync(Guid id)
        {
            return await _context.Documentations.FindAsync(id);
        }

        public async Task<IEnumerable<Documentations>> GetAllAsync(Guid? proyectoId = null)
        {
            if (proyectoId.HasValue)
            {
                return await _context.Documentations
                    .Where(d => d.ProyectoId == proyectoId.Value)
                    .ToListAsync();
            }
            return await _context.Documentations.ToListAsync();
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.Documentations.AnyAsync(d => d.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var documentation = await _context.Documentations.FindAsync(id);
            if (documentation != null)
            {
                _context.Documentations.Remove(documentation);
                await _context.SaveChangesAsync();
            }
        }
    }
}