using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Variable
{
    public class ChangeVariableStatusUseCase
    {
        private readonly IVariableRepository _variableRepository;

        public ChangeVariableStatusUseCase(IVariableRepository variableRepository)
        {
            _variableRepository = variableRepository;
        }

        public async Task<bool> Execute(Guid id, bool isActive)
        {
            return await _variableRepository.ChangeStatusAsync(id, isActive);
        }
    }
}
