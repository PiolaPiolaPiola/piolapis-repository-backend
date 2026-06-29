using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Documentation
{
    public class DeleteDocumentationUseCase
    {
        private readonly IDocumentationRepository _documentationRepository;

        public DeleteDocumentationUseCase(IDocumentationRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        public async Task Execute(Guid id)
        {
            var documentation = await _documentationRepository.GetByIdAsync(id);

            if (documentation == null)
                throw new KeyNotFoundException($"No se encontró la documentación con el Id: {id}");

            await _documentationRepository.Delete(id);
        }
    }
}
