using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.CodeMessage;
using PiolAPIS_Repository.Application.Ports.DTOs;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/mensajes-codigos")]
    public class CodeMessageController : ControllerBase
    {
        private readonly CreateCodeMessageUseCase _createUseCase;
        private readonly GetCodeMessageByIdUseCase _getByIdUseCase;
        private readonly ICodeMessageRepository _repository; 

        public CodeMessageController(
            CreateCodeMessageUseCase createUseCase,
            GetCodeMessageByIdUseCase getByIdUseCase,
            ICodeMessageRepository repository)
        {
            _createUseCase = createUseCase;
            _getByIdUseCase = getByIdUseCase;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CodeMessageDTOs.CreateCodeMessageRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            var nuevoCodigo = new CodeMessages(
                id: null,
                name: request.Name,
                description: request.Description,
                type: request.Type,
                code: request.Code,
                isActive: true,
                createdDate: null,
                updatedDate: null,
                httpCode: request.HttpCode,
                response: request.Response,
                responseType: request.ResponseType
            );

            await _createUseCase.Execute(nuevoCodigo);

            return CreatedAtAction(nameof(GetById), new { id = nuevoCodigo.Id }, nuevoCodigo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeMessages>>> Get()
        {
            var codigos = await _repository.GetAllAsync();
            return Ok(codigos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CodeMessages>> GetById([FromRoute] Guid id)
        {
            try
            {
                var codigo = await _getByIdUseCase.Execute(id);
                return Ok(codigo);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CodeMessageDTOs.UpdateCodeMessageRequest request)
        {
            try
            {
                var codigoExistente = await _getByIdUseCase.Execute(id);
                if (codigoExistente == null)
                    return NotFound($"No se encontró el código de mensaje con ID: {id}");

                codigoExistente.UpdateCodeMessage(
                    request.Name,
                    request.Description,
                    request.HttpCode,
                    request.Response,
                    request.ResponseType
                );

                await _repository.UpdateAsync(codigoExistente);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] CodeMessageDTOs.ChangeCodeMessageStatusRequest request)
        {
            var modificado = await _repository.ChangeStatusAsync(id, request.IsActive);
            if (!modificado)
                return NotFound($"No se pudo cambiar el estado. No existe el código con ID: {id}");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!await _repository.ValidateExistsAsync(id))
                return NotFound($"No existe el código de mensaje con ID: {id}");

            await _repository.Delete(id);
            return NoContent();
        }
    }
}