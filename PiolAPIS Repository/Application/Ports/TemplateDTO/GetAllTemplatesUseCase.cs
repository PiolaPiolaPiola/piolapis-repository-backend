using System.Collections.Generic;
using System.Threading.Tasks;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;

namespace PiolAPIS_Repository.Application.Ports.TemplateDTO
{
    public class GetAllTemplatesUseCase
    {
        private readonly ITemplatesDTOsRepository _repository;

        public GetAllTemplatesUseCase(ITemplatesDTOsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TemplatesDTOs>> Execute()
        {
            return await _repository.GetAllAsync();
        }
    }
}