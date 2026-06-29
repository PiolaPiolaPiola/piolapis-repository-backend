using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Variable
{
    public class UpdateVariableUseCase
    {
        private readonly IVariableRepository _variableRepository;

        public UpdateVariableUseCase(IVariableRepository variableRepository)
        {
            _variableRepository = variableRepository;
        }

        public async Task Execute(Variables variable)
        {
            await _variableRepository.UpdateAsync(variable);
        }
    }
}
