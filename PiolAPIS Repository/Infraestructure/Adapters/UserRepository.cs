using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;
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
            var dbModel = new UsersDbModel
            {
                Id = user.Id,
                Name = user.Name,
                Description = user.Description,
                Type = user.Type,
                Code = user.Code,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate,
                LastName = user.LastName,
                Role = user.Role
            };

            _context.Users.Add(dbModel);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Users?> GetByIdAsync(Guid id)
        {
            var dbModel = await _context.Users.FindAsync(id);
            if (dbModel == null) return null;

            return MapToDomain(dbModel);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            var dbModels = await _context.Users.ToListAsync();
            return dbModels.Select(MapToDomain);
        }

        public async Task<IEnumerable<Users>> GetAllByRoleAsync(string role)
        {
            var dbModels = await _context.Users
                .Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return dbModels.Select(MapToDomain);
        }

        public async Task<Users> UpdateAsync(Users user)
        {
            var dbModel = await _context.Users.FindAsync(user.Id);
            if (dbModel == null)
                throw new KeyNotFoundException($"No se encontró el registro para actualizar con ID: {user.Id}");

            dbModel.Name = user.Name;
            dbModel.Description = user.Description;
            dbModel.Type = user.Type;
            dbModel.Code = user.Code;
            dbModel.IsActive = user.IsActive;
            dbModel.UpdatedDate = user.UpdatedDate;
            dbModel.LastName = user.LastName;
            dbModel.Role = user.Role;

            _context.Users.Update(dbModel);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var dbModel = await _context.Users.FindAsync(id);
            if (dbModel == null) return false;

            var domainUser = MapToDomain(dbModel);

            if (isActive)
                domainUser.Activate();
            else
                domainUser.Deactivate();

            dbModel.IsActive = domainUser.IsActive;
            dbModel.UpdatedDate = domainUser.UpdatedDate;

            _context.Users.Update(dbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Delete(Guid id)
        {
            var dbModel = await _context.Users.FindAsync(id);
            if (dbModel != null)
            {
                _context.Users.Remove(dbModel);
                await _context.SaveChangesAsync();
            }
        }

        private static Users MapToDomain(UsersDbModel dbModel)
        {
            return new Users(
                id: dbModel.Id,
                name: dbModel.Name,
                description: dbModel.Description,
                type: dbModel.Type,
                code: dbModel.Code,
                isActive: dbModel.IsActive,
                createdDate: dbModel.CreatedDate,
                updatedDate: dbModel.UpdatedDate,
                lastName: dbModel.LastName,
                role: dbModel.Role
            );
        }
    }
}