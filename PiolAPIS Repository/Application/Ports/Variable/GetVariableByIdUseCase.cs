using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.Variable
{
    public class GetVariableByIdUseCase
    {
        private readonly IVariableRepository _variableRepository;

        public GetVariableByIdUseCase(IVariableRepository variableRepository)
        {
            _variableRepository = variableRepository;
        }

        public async Task<Variables?> Execute(Guid id)
        {
            var variable = await _variableRepository.GetByIdAsync(id);

            if (variable == null)
                throw new KeyNotFoundException($"No se encontró la variable con el Id: {id}");

            return variable;
        }
    }
}
