using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;
using PiolAPIS_Repository.Application.Ports.DTOs;
using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateDTOsController : ControllerBase
    {
        private readonly CreateTemplatesDTOsUseCase _createUseCase;
        private readonly GetTemplateByIdUseCase _getByIdUseCase;
        private readonly GetAllTemplatesUseCase _getAllUseCase;
        private readonly UpdateTemplateUseCase _updateUseCase;
        private readonly DeleteTemplateUseCase _deleteUseCase;
        private readonly ChangeTemplateStatusUseCase _changeStatusUseCase;

        public TemplateDTOsController(
            CreateTemplatesDTOsUseCase createUseCase,
            GetTemplateByIdUseCase getByIdUseCase,
            GetAllTemplatesUseCase getAllUseCase,
            UpdateTemplateUseCase updateUseCase,
            DeleteTemplateUseCase deleteUseCase,
            ChangeTemplateStatusUseCase changeStatusUseCase)
        {
            _createUseCase = createUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
            _changeStatusUseCase = changeStatusUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TemplateDTOs.CreateTemplateRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición no puede estar vacío.");

            var nuevaPlantilla = new TemplatesDTOs(
                id: null,
                name: request.Name,
                description: request.Description,
                type: request.Type,
                code: request.Code,
                isActive: true,
                createdDate: null,
                updatedDate: null,
                requestType: request.RequestType,
                request: request.Request,
                response: request.Response,
                responseType: request.ResponseType,
                isShared: request.IsShared,
                tags: request.Tags
            );

            await _createUseCase.Execute(nuevaPlantilla);

            return CreatedAtAction(nameof(GetById), new { id = nuevaPlantilla.Id }, nuevaPlantilla);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TemplatesDTOs>> GetById([FromRoute] Guid id)
        {
            try
            {
                var plantilla = await _getByIdUseCase.Execute(id);
                return Ok(plantilla);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplatesDTOs>>> Get([FromQuery] bool? includeInactive = false)
        {
            var plantillas = await _getAllUseCase.Execute();

            if (!includeInactive.Value)
            {
                plantillas = plantillas.Where(p => p.IsActive);
            }
            return Ok(plantillas);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TemplateDTOs.UpdateTemplateRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición no puede estar vacío.");

            try
            {
                var plantillaExistente = await _getByIdUseCase.Execute(id);

                var plantillaActualizada = new TemplatesDTOs(
                    id: id,
                    name: request.Name,
                    description: request.Description,
                    type: request.Type,
                    code: request.Code,
                    isActive: plantillaExistente.IsActive, 
                    createdDate: plantillaExistente.CreatedDate, 
                    updatedDate: DateTime.UtcNow, 
                    requestType: request.RequestType,
                    request: request.Request,
                    response: request.Response,
                    responseType: request.ResponseType,
                    isShared: plantillaExistente.IsShared, 
                    tags: request.Tags
                );

                await _updateUseCase.Execute(plantillaActualizada);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] UserDTOs.ChangeStatusRequest request)
        {
            var modificado = await _changeStatusUseCase.Execute(id, request.IsActive);
            if (!modificado)
                return NotFound($"No se pudo cambiar el estado. No existe la plantilla con ID: {id}");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _getByIdUseCase.Execute(id);
                await _deleteUseCase.Execute(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}