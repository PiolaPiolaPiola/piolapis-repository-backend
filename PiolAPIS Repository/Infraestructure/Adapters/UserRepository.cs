using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.User;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class UserRepository : IUserRepository
    {
        private readonly PiolapisDbContext _context;

        public UserRepository(PiolapisDbContext context) 
        {
            _context = context;
        }

        public async Task<Users> SaveAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Users?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            return user != null;
        }

        public async Task<IEnumerable<Users>> GetAllByRoleAsync(string role)
        {
            return await _context.Users
                .Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<Users> UpdateAsync(Users user)
        {
            await ValidateExistsAsync(user.Id.Value);
            _context.Update(user);
            return user;
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            if (isActive)
                user.Activate(); 
            else
                user.Deactivate(); 

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
}
}
