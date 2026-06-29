using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Documentation
{
    public class UpdateDocumentationUseCase
    {
        private readonly IDocumentationRepository _documentationRepository;

        public UpdateDocumentationUseCase(IDocumentationRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        public async Task Execute(Documentations documentation)
        {
            await _documentationRepository.UpdateAsync(documentation);
        }
    }
}
