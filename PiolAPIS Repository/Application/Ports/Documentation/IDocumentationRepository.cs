using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Documentation
{
    public interface IDocumentationRepository
    {
        Task SaveAsync(Documentations documentation);
        Task<Documentations?> GetByIdAsync(Guid id);
        Task<IEnumerable<Documentations>> GetAllAsync(Guid? proyectoId = null);
        Task UpdateAsync(Documentations documentation);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<bool> ValidateExistsAsync(Guid id);
        Task Delete(Guid id);
    }
}
