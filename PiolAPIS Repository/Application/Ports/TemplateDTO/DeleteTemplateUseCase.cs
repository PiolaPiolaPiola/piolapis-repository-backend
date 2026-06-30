using System;
using System.Threading.Tasks;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;

namespace PiolAPIS_Repository.Application.Ports.TemplateDTO
{
    public class DeleteTemplateUseCase
    {
        private readonly ITemplatesDTOsRepository _repository;

        public DeleteTemplateUseCase(ITemplatesDTOsRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(Guid id)
        {
            await _repository.Delete(id);
        }
    }
}