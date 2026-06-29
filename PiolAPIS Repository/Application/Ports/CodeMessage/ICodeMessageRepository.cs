using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.CodeMessage
{
    public interface ICodeMessageRepository
    {
        Task SaveAsync(CodeMessages codeMessage);
        Task<CodeMessages?> GetByIdAsync(Guid id);
        Task<IEnumerable<CodeMessages>> GetAllByHttpCodeAsync(string httpCode);
        Task<IEnumerable<CodeMessages>> GetAllAsync();
        Task UpdateAsync(CodeMessages codeMessage);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<bool> ValidateExistsAsync(Guid id);
        Task Delete(Guid id);
    }
}
