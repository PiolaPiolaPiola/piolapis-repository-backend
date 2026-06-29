using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.DocumentationSetting
{
    public class GetDocumentationSettingByIdUseCase
    {
        private readonly IDocumentationSettingRepository _documentationSettingRepository;

        public GetDocumentationSettingByIdUseCase(IDocumentationSettingRepository documentationSettingRepository)
        {
            _documentationSettingRepository = documentationSettingRepository;
        }

        public async Task<DocumentationSettings?> Execute(Guid id)
        {
            var setting = await _documentationSettingRepository.GetByIdAsync(id);

            if (setting == null)
                throw new KeyNotFoundException($"No se encontró la configuración de documentación con el Id: {id}");

            return setting;
        }
    }
}
