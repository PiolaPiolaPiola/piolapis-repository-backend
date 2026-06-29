using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Project
{
    public class UpdateProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Projects> Execute(Projects proyecto)
        {
            return await _projectRepository.UpdateAsync(proyecto);
        }
    }
}
