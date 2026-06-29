using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.DocumentationSetting
{
    public interface IDocumentationSettingRepository
    {
        Task SaveAsync(DocumentationSettings documentationSetting);
        Task<DocumentationSettings?> GetByIdAsync(Guid id);
        Task<IEnumerable<DocumentationSettings>> GetAllAsync();
        Task UpdateAsync(DocumentationSettings documentationSetting);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<bool> ValidateExistsAsync(Guid id);
        Task Delete(Guid id);
    }
}
