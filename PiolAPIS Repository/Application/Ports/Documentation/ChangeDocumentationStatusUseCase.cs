using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Documentation
{
    public class ChangeDocumentationStatusUseCase
    {
        private readonly IDocumentationRepository _documentationRepository;

        public ChangeDocumentationStatusUseCase(IDocumentationRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        public async Task<bool> Execute(Guid id, bool isActive)
        {
            return await _documentationRepository.ChangeStatusAsync(id, isActive);
        }
    }
}
