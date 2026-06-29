using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.DocumentationSetting
{
    public class GetAllDocumentationSettingsUseCase
    {
        private readonly IDocumentationSettingRepository _documentationSettingRepository;

        public GetAllDocumentationSettingsUseCase(IDocumentationSettingRepository documentationSettingRepository)
        {
            _documentationSettingRepository = documentationSettingRepository;
        }

        public async Task<IEnumerable<DocumentationSettings>> Execute()
        {
            return await _documentationSettingRepository.GetAllAsync();
        }
    }
}
