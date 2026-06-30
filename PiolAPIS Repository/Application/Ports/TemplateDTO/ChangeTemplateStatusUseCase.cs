using System;
using System.Threading.Tasks;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;

namespace PiolAPIS_Repository.Application.Ports.TemplateDTO
{
    public class ChangeTemplateStatusUseCase
    {
        private readonly ITemplatesDTOsRepository _repository;

        public ChangeTemplateStatusUseCase(ITemplatesDTOsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Execute(Guid id, bool isActive)
        {
            return await _repository.ChangeStatusAsync(id, isActive);
        }
    }
}