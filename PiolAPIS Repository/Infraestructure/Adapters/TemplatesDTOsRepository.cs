using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class TemplatesDTOsRepository : ITemplatesDTOsRepository
    {
        private readonly PiolapisDbContext _context;

        public TemplatesDTOsRepository(PiolapisDbContext context) 
        {
            _context = context;
        }

        public async Task SaveAsync(TemplatesDTOs templateDTO)
        {
            _context.TemplatesDTOs.Add(templateDTO);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TemplatesDTOs templateDTO)
        {
            _context.Update(templateDTO);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var templateDTO = await GetByIdAsync(id);
            if (templateDTO == null) return false;
            templateDTO.IsActive = isActive;
            templateDTO.UpdatedDate = DateTime.UtcNow;
            _context.Update(templateDTO);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TemplatesDTOs?> GetByIdAsync(Guid id)
        {
            return await _context.TemplatesDTOs.FindAsync(id);
        }

        public async Task<IEnumerable<TemplatesDTOs>> GetAllAsync()
        {
            return await _context.TemplatesDTOs.ToListAsync();
        }
    }
}
