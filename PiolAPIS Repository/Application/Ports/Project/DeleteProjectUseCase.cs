using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Project
{
    public class DeleteProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Execute(Guid id)
        {
            var proyecto = await _projectRepository.GetByIdAsync(id);

            if (proyecto == null)
                throw new KeyNotFoundException($"No se encontró el proyecto con el Id: {id}");

            await _projectRepository.Delete(id);
        }
    }
}
