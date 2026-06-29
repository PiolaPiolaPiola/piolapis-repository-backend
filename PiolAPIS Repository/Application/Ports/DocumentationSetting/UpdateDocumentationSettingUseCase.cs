using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.DocumentationSetting
{
    public class UpdateDocumentationSettingUseCase
    {
        private readonly IDocumentationSettingRepository _documentationSettingRepository;

        public UpdateDocumentationSettingUseCase(IDocumentationSettingRepository documentationSettingRepository)
        {
            _documentationSettingRepository = documentationSettingRepository;
        }

        public async Task Execute(DocumentationSettings documentationSetting)
        {
            await _documentationSettingRepository.UpdateAsync(documentationSetting);
        }
    }
}
