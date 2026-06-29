using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.User
{
    public interface IUserRepository
    {
        Task<Users> SaveAsync(Users user);
        Task<Users?> GetByIdAsync(Guid id);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<IEnumerable<Users>> GetAllByRoleAsync(string role);
        Task<Users> UpdateAsync(Users user);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<bool> ValidateExistsAsync(Guid id);
        Task Delete(Guid id);
    }
}
