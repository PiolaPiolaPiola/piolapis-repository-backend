using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public interface IVariableRepository
    {
        Task SaveAsync(Variable variable);
        Task UpdateAsync(Variable variable);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<Variable?> GetByIdAsync(Guid id);
        Task<IEnumerable<Variable>> GetAllAsync();
    }
}
