using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Project
{
    public interface IProjectRepository
    {
        Task<Projects> SaveAsync(Projects project);
        Task<Projects?> GetByIdAsync(Guid id);
        Task<IEnumerable<Projects>> GetAllAsync();
        Task<Projects> UpdateAsync(Projects project);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<bool> ValidateExistsAsync(Guid id);
        Task Delete(Guid id);
    }
}
