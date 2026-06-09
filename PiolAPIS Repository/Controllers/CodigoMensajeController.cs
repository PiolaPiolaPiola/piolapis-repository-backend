using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Models;
using PiolAPIS_Repository.Models.Enums;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/mensajes-codigos")]
    public class CodigoMensajeController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<CodigoMensaje>> Post([FromBody] CodigoMensaje modelo)
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
        public async Task<ActionResult<IEnumerable<CodigoMensaje>>> Get()
        {
            // TODO: Consultar el diccionario de respuestas parametrizadas de la BD de forma asíncrona
            // var codigos = await _repository.GetAllAsync();

            List<CodigoMensaje> listaSimulada = [];

            return Ok(listaSimulada);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CodigoMensaje>> GetById([FromRoute] Guid id)
        {
            // TODO: Buscar el código parametrizado por su GUID
            // var codigo = await _repository.GetByIdAsync(id);
            // if (codigo == null) return NotFound($"No se encontró el código de mensaje con ID: {id}");

            CodigoMensaje modeloSimulado = new()
            {
                Id = id,
                Name = "ERR_USER_NOT_FOUND",
                Description = "Error global cuando un identificador de usuario no existe en el sistema.",
                HTTP_code = "404",
                Response = "{ \"code\": \"404\", \"message\": \"El recurso solicitado no fue hallado.\" }",
                ResponseType = (char)TipoDocumentacion.JSON, // 'J' para JSON, 'S' para Schema, etc
                IsActive = true,                                             
                CreatedDate = DateTime.UtcNow.AddMonths(-1),
                UpdatedDate = DateTime.UtcNow
            };

            return Ok(modeloSimulado);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CodigoMensaje modelo)
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
