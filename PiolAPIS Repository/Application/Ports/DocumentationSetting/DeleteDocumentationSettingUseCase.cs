using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.DocumentationSetting
{
    public class DeleteDocumentationSettingUseCase
    {
        private readonly IDocumentationSettingRepository _documentationSettingRepository;

        public DeleteDocumentationSettingUseCase(IDocumentationSettingRepository documentationSettingRepository)
        {
            _documentationSettingRepository = documentationSettingRepository;
        }

        public async Task Execute(Guid id)
        {
            var setting = await _documentationSettingRepository.GetByIdAsync(id);

            if (setting == null)
                throw new KeyNotFoundException($"No se encontró la configuración de documentación con el Id: {id}");

            await _documentationSettingRepository.Delete(id);
        }
    }
}
