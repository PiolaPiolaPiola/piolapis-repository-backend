using System;
using System.Threading.Tasks;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;

namespace PiolAPIS_Repository.Application.Ports.TemplateDTO
{
    public class GetTemplateByIdUseCase
    {
        private readonly ITemplatesDTOsRepository _repository;

        public GetTemplateByIdUseCase(ITemplatesDTOsRepository repository)
        {
            _repository = repository;
        }

        public async Task<TemplatesDTOs> Execute(Guid id)
        {
            var template = await _repository.GetByIdAsync(id);
            if (template == null)
                throw new KeyNotFoundException($"No se encontró la plantilla DTO con ID: {id}");

            return template;
        }
    }
}