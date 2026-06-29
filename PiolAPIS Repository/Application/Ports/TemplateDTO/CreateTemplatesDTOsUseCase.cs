using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.TemplateDTO
{
    public class CreateTemplatesDTOsUseCase
    {
        private readonly ITemplatesDTOsRepository _templatesDTOsRepository;

        public CreateTemplatesDTOsUseCase(ITemplatesDTOsRepository templatesDTOsRepository)
        {
            _templatesDTOsRepository = templatesDTOsRepository;
        }

        public async Task Execute(TemplatesDTOs modelo)
        {
            await _templatesDTOsRepository.SaveAsync(modelo);
        }
    }
}
