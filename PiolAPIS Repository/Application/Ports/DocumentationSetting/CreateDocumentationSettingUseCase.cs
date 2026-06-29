using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.DocumentationSetting
{
    public class CreateDocumentationSettingUseCase
    {
        private readonly IDocumentationSettingRepository _documentationSettingRepository;

        public CreateDocumentationSettingUseCase(IDocumentationSettingRepository documentationSettingRepository)
        {
            _documentationSettingRepository = documentationSettingRepository;
        }

        public async Task Execute(DocumentationSettings documentationSetting)
        {
            await _documentationSettingRepository.SaveAsync(documentationSetting);
        }
    }
}
