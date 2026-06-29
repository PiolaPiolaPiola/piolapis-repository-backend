using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Variable
{
    public interface IVariableRepository
    {
        Task SaveAsync(Variables variable);
        Task<Variables?> GetByIdAsync(Guid id);
        Task<IEnumerable<Variables>> GetAllAsync();
        Task UpdateAsync(Variables variable);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<bool> ValidateExistsAsync(Guid id);
        Task Delete(Guid id);
    }
}
