using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.CodeMessage;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class CodeMessageRepository : ICodeMessageRepository
    {
        private readonly PiolapisDbContext _context;

        public CodeMessageRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(CodeMessages codeMessage)
        {
            _context.CodeMessages.Add(codeMessage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CodeMessages codeMessage)
        {
            _context.Update(codeMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var codeMessage = await _context.CodeMessages.FindAsync(id);
            if (codeMessage == null) return false;

            if (isActive)
                codeMessage.Activate();
            else
                codeMessage.Deactivate();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CodeMessages?> GetByIdAsync(Guid id)
        {
            return await _context.CodeMessages.FindAsync(id);
        }

        public async Task<IEnumerable<CodeMessages>> GetAllAsync()
        {
            return await _context.CodeMessages.ToListAsync();
        }

        public async Task<IEnumerable<CodeMessages>> GetAllByHttpCodeAsync(string httpCode)
        {
            return await _context.CodeMessages
                .Where(cm => cm.HttpCode.Equals(httpCode, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.CodeMessages.AnyAsync(cm => cm.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var codeMessage = await _context.CodeMessages.FindAsync(id);
            if (codeMessage != null)
            {
                _context.CodeMessages.Remove(codeMessage);
                await _context.SaveChangesAsync();
            }
        }
    }
}