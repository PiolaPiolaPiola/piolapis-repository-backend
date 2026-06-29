using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Documentation
{
    public class GetDocumentationByIdUseCase
    {
        private readonly IDocumentationRepository _documentationRepository;

        public GetDocumentationByIdUseCase(IDocumentationRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        public async Task<Documentations?> Execute(Guid id)
        {
            var documentation = await _documentationRepository.GetByIdAsync(id);

            if (documentation == null)
                throw new KeyNotFoundException($"No se encontró la documentación con el Id: {id}");

            return documentation;
        }
    }
}
