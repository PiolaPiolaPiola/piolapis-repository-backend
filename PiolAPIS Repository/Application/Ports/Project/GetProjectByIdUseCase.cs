using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Project
{
    public class GetProjectByIdUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Projects?> Execute(Guid id)
        {
            var proyecto = await _projectRepository.GetByIdAsync(id);

            if (proyecto == null)
                throw new KeyNotFoundException($"No se encontró el proyecto con el Id: {id}");

            return proyecto;
        }
    }
}
