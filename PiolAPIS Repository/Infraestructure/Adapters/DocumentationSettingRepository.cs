using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.DocumentationSetting;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class DocumentationSettingRepository : IDocumentationSettingRepository
    {
        private readonly PiolapisDbContext _context;

        public DocumentationSettingRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(DocumentationSettings documentationSetting)
        {
            _context.DocumentationSettings.Add(documentationSetting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocumentationSettings documentationSetting)
        {
            _context.Update(documentationSetting);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var documentationSetting = await _context.DocumentationSettings.FindAsync(id);
            if (documentationSetting == null) return false;

            if (isActive)
                documentationSetting.Activate();
            else
                documentationSetting.Deactivate();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DocumentationSettings?> GetByIdAsync(Guid id)
        {
            return await _context.DocumentationSettings.FindAsync(id);
        }

        public async Task<IEnumerable<DocumentationSettings>> GetAllAsync()
        {
            return await _context.DocumentationSettings.ToListAsync();
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.DocumentationSettings.AnyAsync(ds => ds.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var setting = await _context.DocumentationSettings.FindAsync(id);
            if (setting != null)
            {
                _context.DocumentationSettings.Remove(setting);
                await _context.SaveChangesAsync();
            }
        }
    }
}