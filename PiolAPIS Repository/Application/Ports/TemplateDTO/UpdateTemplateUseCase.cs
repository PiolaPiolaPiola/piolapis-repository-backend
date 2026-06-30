using System.Threading.Tasks;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;

namespace PiolAPIS_Repository.Application.Ports.TemplateDTO
{
    public class UpdateTemplateUseCase
    {
        private readonly ITemplatesDTOsRepository _repository;

        public UpdateTemplateUseCase(ITemplatesDTOsRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(TemplatesDTOs template)
        {
            await _repository.UpdateAsync(template);
        }
    }
}