using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Documentation
{
    public class GetAllDocumentationsUseCase
    {
        private readonly IDocumentationRepository _documentationRepository;

        public GetAllDocumentationsUseCase(IDocumentationRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        public async Task<IEnumerable<Documentations>> Execute(Guid? proyectoId = null)
        {
            return await _documentationRepository.GetAllAsync(proyectoId);
        }
    }
}
