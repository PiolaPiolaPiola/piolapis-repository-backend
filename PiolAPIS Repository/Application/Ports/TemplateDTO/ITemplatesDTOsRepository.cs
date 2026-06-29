using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.TemplateDTO
{
    public interface ITemplatesDTOsRepository
    {
        Task SaveAsync(TemplatesDTOs templateDTO);
        Task<TemplatesDTOs?> GetByIdAsync(Guid id);
        Task<IEnumerable<TemplatesDTOs>> GetAllAsync();
        Task UpdateAsync(TemplatesDTOs templateDTO);
        Task<bool> ChangeStatusAsync(Guid id, bool isActive);
        Task<bool> ValidateExistsAsync(Guid id);
        Task Delete(Guid id);
    }
}
