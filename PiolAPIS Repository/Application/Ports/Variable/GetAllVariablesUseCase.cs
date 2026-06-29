using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Variable
{
    public class GetAllVariablesUseCase
    {
        private readonly IVariableRepository _variableRepository;

        public GetAllVariablesUseCase(IVariableRepository variableRepository)
        {
            _variableRepository = variableRepository;
        }

        public async Task<IEnumerable<Variables>> Execute()
        {
            return await _variableRepository.GetAllAsync();
        }
    }
}
