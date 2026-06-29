using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.CodeMessage
{
    public class GetCodeMessageByIdUseCase
    {
        private readonly ICodeMessageRepository _codeMessageRepository;

        public GetCodeMessageByIdUseCase(ICodeMessageRepository codeMessageRepository)
        {
            _codeMessageRepository = codeMessageRepository;
        }

        public async Task<CodeMessages?> Execute(Guid id)
        {
            var codeMessage = await _codeMessageRepository.GetByIdAsync(id);

            if (codeMessage == null)
                throw new KeyNotFoundException($"No se encontró el mensaje código con el Id: {id}");

            return codeMessage;
        }
    }
}
