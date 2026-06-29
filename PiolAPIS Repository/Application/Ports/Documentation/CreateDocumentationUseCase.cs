using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Documentation
{
    public class CreateDocumentationUseCase
    {
        private readonly IDocumentationRepository _documentationRepository;

        public CreateDocumentationUseCase(IDocumentationRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        public async Task Execute(Documentations documentation)
        {
            await _documentationRepository.SaveAsync(documentation);
        }
    }
}
