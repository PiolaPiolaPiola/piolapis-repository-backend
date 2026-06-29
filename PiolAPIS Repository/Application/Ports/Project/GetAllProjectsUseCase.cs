using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Project
{
    public class GetAllProjectsUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Projects>> Execute()
        {
            return await _projectRepository.GetAllAsync();
        }
    }
}
