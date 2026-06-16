using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Out
{
    public interface IDocumentationRepository
    {
        Task SaveAsync(Documentation documentation);
        Task UpdateAsync(Documentation documentation);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<Documentation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Documentation>> GetAllAsync(Guid? proyectoId = null);
    }
}
