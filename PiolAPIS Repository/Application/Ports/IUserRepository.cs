using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public interface IUserRepository
    {
        Task SaveAsync(User user);
        Task UpdateAsync(User user);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<User?> GetByIdAsync(Guid id); 
        Task<IEnumerable<User>> GetAllByRoleAsync(string role);
    }
}
