using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.CodeMessage
{
    public class CreateCodeMessageUseCase
    {
        private readonly ICodeMessageRepository _codeMessageRepository;

        public CreateCodeMessageUseCase(ICodeMessageRepository codeMessageRepository)
        {
            _codeMessageRepository = codeMessageRepository;
        }

        public async Task Execute(CodeMessages codeMessage)
        {
            await _codeMessageRepository.SaveAsync(codeMessage);
        }
    }
}
