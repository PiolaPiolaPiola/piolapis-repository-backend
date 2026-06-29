using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.DocumentationSetting;
using PiolAPIS_Repository.Application.Ports.DTOs;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/configuraciones-documentacion")]
    public class DocumentationSettingController : ControllerBase
    {
        private readonly CreateDocumentationSettingUseCase _createUseCase;
        private readonly GetDocumentationSettingByIdUseCase _getByIdUseCase;
        private readonly GetAllDocumentationSettingsUseCase _getAllUseCase;
        private readonly UpdateDocumentationSettingUseCase _updateUseCase;
        private readonly DeleteDocumentationSettingUseCase _deleteUseCase;
        private readonly ChangeDocumentationSettingStatusUseCase _changeStatusUseCase;

        public DocumentationSettingController(
            CreateDocumentationSettingUseCase createUseCase,
            GetDocumentationSettingByIdUseCase getByIdUseCase,
            GetAllDocumentationSettingsUseCase getAllUseCase,
            UpdateDocumentationSettingUseCase updateUseCase,
            DeleteDocumentationSettingUseCase deleteUseCase,
            ChangeDocumentationSettingStatusUseCase changeStatusUseCase)
        {
            _createUseCase = createUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
            _changeStatusUseCase = changeStatusUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DocumentationSettingDTOs.CreateDocumentationSettingRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            var nuevaConfiguracion = new DocumentationSettings(
                id: null,
                name: request.Name,
                description: request.Description,
                type: request.Type,
                code: request.Code,
                isActive: true,
                createdDate: null,
                updatedDate: null,
                baseEndpoint: request.BaseEndpoint,
                apiType: request.ApiType,
                proyectoId: request.ProyectoId
            );

            await _createUseCase.Execute(nuevaConfiguracion);

            return CreatedAtAction(nameof(GetById), new { id = nuevaConfiguracion.Id }, nuevaConfiguracion);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentationSettings>>> Get()
        {
            var lista = await _getAllUseCase.Execute();
            return Ok(lista);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DocumentationSettings>> GetById([FromRoute] Guid id)
        {
            try
            {
                var configuracion = await _getByIdUseCase.Execute(id);
                return Ok(configuracion);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] DocumentationSettingDTOs.UpdateDocumentationSettingRequest request)
        {
            try
            {
                var configExistente = await _getByIdUseCase.Execute(id);
                if (configExistente == null)
                    return NotFound($"No se encontró la configuración con ID: {id}");

                configExistente.UpdateSetting(request.Name, request.Description, request.BaseEndpoint, request.ApiType);

                await _updateUseCase.Execute(configExistente);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] DocumentationSettingDTOs.ChangeDocumentationSettingStatusRequest request)
        {
            var modificado = await _changeStatusUseCase.Execute(id, request.IsActive);
            if (!modificado)
                return NotFound($"No se pudo cambiar el estado. No existe la configuración con ID: {id}");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
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