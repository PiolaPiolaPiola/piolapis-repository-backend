using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.Documentation;
using PiolAPIS_Repository.Application.Ports.DTOs;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/documentaciones")]
    public class DocumentationController : ControllerBase
    {
        private readonly CreateDocumentationUseCase _createDocumentationUseCase;
        private readonly GetDocumentationByIdUseCase _getDocumentationByIdUseCase;
        private readonly GetAllDocumentationsUseCase _getAllDocumentationsUseCase;
        private readonly UpdateDocumentationUseCase _updateDocumentationUseCase;
        private readonly DeleteDocumentationUseCase _deleteDocumentationUseCase;
        private readonly ChangeDocumentationStatusUseCase _changeDocumentationStatusUseCase;

        public DocumentationController(
            CreateDocumentationUseCase createDocumentationUseCase,
            GetDocumentationByIdUseCase getDocumentationByIdUseCase,
            GetAllDocumentationsUseCase getAllDocumentationsUseCase,
            UpdateDocumentationUseCase updateDocumentationUseCase,
            DeleteDocumentationUseCase deleteDocumentationUseCase,
            ChangeDocumentationStatusUseCase changeDocumentationStatusUseCase)
        {
            _createDocumentationUseCase = createDocumentationUseCase;
            _getDocumentationByIdUseCase = getDocumentationByIdUseCase;
            _getAllDocumentationsUseCase = getAllDocumentationsUseCase;
            _updateDocumentationUseCase = updateDocumentationUseCase;
            _deleteDocumentationUseCase = deleteDocumentationUseCase;
            _changeDocumentationStatusUseCase = changeDocumentationStatusUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DocumentationDTOs.CreateDocumentationRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            var nuevaDocumentacion = new Documentations(
                id: null,
                name: request.Name,
                description: request.Description,
                type: request.Type,
                code: request.Code,
                isActive: true,
                createdDate: null,
                updatedDate: null,
                proyectoId: request.ProyectoId,
                configuracionDocumentacionId: request.ConfiguracionDocumentacionId,
                plantillaDtoId: request.PlantillaDtoId,
                version: request.Version
            );

            await _createDocumentationUseCase.Execute(nuevaDocumentacion);

            return CreatedAtAction(nameof(GetById), new { id = nuevaDocumentacion.Id }, nuevaDocumentacion);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documentations>>> Get([FromQuery] Guid? proyectoId)
        {
            var documentaciones = await _getAllDocumentationsUseCase.Execute(proyectoId);
            return Ok(documentaciones);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Documentations>> GetById([FromRoute] Guid id)
        {
            try
            {
                var documentation = await _getDocumentationByIdUseCase.Execute(id);
                return Ok(documentation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] DocumentationDTOs.UpdateDocumentationRequest request)
        {
            try
            {
                var docExistente = await _getDocumentationByIdUseCase.Execute(id);
                if (docExistente == null)
                    return NotFound($"No existe documentación con el ID: {id}");

                docExistente.UpdateDocumentation(request.Name, request.Description, request.Version, request.PlantillaDtoId);

                await _updateDocumentationUseCase.Execute(docExistente);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] DocumentationDTOs.ChangeDocumentationStatusRequest request)
        {
            var modificado = await _changeDocumentationStatusUseCase.Execute(id, request.IsActive);
            if (!modificado)
                return NotFound($"No se pudo cambiar el estado. No existe la documentación con ID: {id}");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _deleteDocumentationUseCase.Execute(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}