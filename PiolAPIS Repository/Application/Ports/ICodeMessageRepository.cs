using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public interface ICodeMessageRepository
    {
        Task SaveAsync(CodeMessage codeMessage);
        Task UpdateAsync(CodeMessage codeMessage);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<CodeMessage?> GetByIdAsync(Guid id); 
        Task<IEnumerable<CodeMessage>> GetAllByHttpCodeAsync(string httpCode);
    }
}
