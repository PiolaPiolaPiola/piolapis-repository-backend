using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Domain.Entities.Enums;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/mensajes-codigos")]
    public class CodeMessageController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<CodeMessage>> Post([FromBody] CodeMessage modelo)
        {
            if (modelo == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            modelo.Id ??= Guid.NewGuid();
            modelo.CreatedDate = DateTime.UtcNow;
            modelo.UpdatedDate = DateTime.UtcNow;

            // TODO: Validar que el HTTP_code sea válido y guardar en la BD
            // await _repository.InsertAsync(modelo);

            return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeMessage>>> Get()
        {
            // TODO: Consultar el diccionario de respuestas parametrizadas de la BD de forma asíncrona
            // var codigos = await _repository.GetAllAsync();

            List<CodeMessage> listaSimulada = [];

            return Ok(listaSimulada);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CodeMessage>> GetById([FromRoute] Guid id)
        {
            // TODO: Buscar el código parametrizado por su GUID
            // var codigo = await _repository.GetByIdAsync(id);
            // if (codigo == null) return NotFound($"No se encontró el código de mensaje con ID: {id}");

            CodeMessage modeloSimulado = new CodeMessage(
                id: id,
                name: "ERR_USER_NOT_FOUND",
                description: "Error global cuando un identificador de usuario no existe en el sistema.",
                isActive: true,
                createdDate: DateTime.UtcNow.AddMonths(-1),
                updatedDate: DateTime.UtcNow,
                httpCode: "404",
                response: "{ \"code\": \"404\", \"message\": \"El recurso solicitado no fue hallado.\" }",
                responseType: (char)DocumentationType.JSON
            );

            return Ok(modeloSimulado);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CodeMessage modelo)
        {
            if (id != modelo.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del modelo enviado.");

            // TODO: Mapear y actualizar las propiedades mutables (Description, Response, Response_type, HTTP_code)
            modelo.UpdatedDate = DateTime.UtcNow;

            // var actualizado = await _repository.UpdateAsync(modelo);
            // if (!actualizado) return NotFound();

            return NoContent(); 
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // var eliminado = await _repository.DeletePhysicalAsync(id);
            // if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
