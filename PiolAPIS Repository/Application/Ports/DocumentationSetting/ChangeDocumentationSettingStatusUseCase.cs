using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.DocumentationSetting
{
    public class ChangeDocumentationSettingStatusUseCase
    {
        private readonly IDocumentationSettingRepository _documentationSettingRepository;

        public ChangeDocumentationSettingStatusUseCase(IDocumentationSettingRepository documentationSettingRepository)
        {
            _documentationSettingRepository = documentationSettingRepository;
        }

        public async Task<bool> Execute(Guid id, bool isActive)
        {
            return await _documentationSettingRepository.ChangeStatusAsync(id, isActive);
        }
    }
}
