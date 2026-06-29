using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Variable
{
    public class CreateVariableUseCase
    {
        private readonly IVariableRepository _variableRepository;

        public CreateVariableUseCase(IVariableRepository variableRepository)
        {
            _variableRepository = variableRepository;
        }

        public async Task Execute(Variables variable)
        {
            await _variableRepository.SaveAsync(variable);
        }
    }
}
