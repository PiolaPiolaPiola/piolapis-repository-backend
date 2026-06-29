using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.Variable;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class VariableRepository : IVariableRepository
    {
        private readonly PiolapisDbContext _context;

        public VariableRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Variables variable)
        {
            _context.Variables.Add(variable);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Variables variable)
        {
            _context.Update(variable);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var variable = await _context.Variables.FindAsync(id);
            if (variable == null) return false;

            if (isActive)
                variable.Activate();
            else
                variable.Deactivate();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Variables?> GetByIdAsync(Guid id)
        {
            return await _context.Variables.FindAsync(id);
        }

        public async Task<IEnumerable<Variables>> GetAllAsync()
        {
            return await _context.Variables.ToListAsync();
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.Variables.AnyAsync(v => v.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var variable = await _context.Variables.FindAsync(id);
            if (variable != null)
            {
                _context.Variables.Remove(variable);
                await _context.SaveChangesAsync();
            }
        }
    }
}