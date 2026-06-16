using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Out
{
    public interface IProyectRepository
    {
        Task SaveAsync(Project project);
        Task UpdateAsync(Project project);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllAsync();
    }
}
