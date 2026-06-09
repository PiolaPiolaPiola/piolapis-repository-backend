using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Models;
using PiolAPIS_Repository.Models.Enums;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/configuraciones-documentacion")]
    public class ConfiguracionDocumentacionController : Controller
    {
        public async Task<ActionResult<ConfiguracionDocumentacion>> Post([FromBody] ConfiguracionDocumentacion modelo)
        {
            if (modelo == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            if (!modelo.Id.HasValue)
            {
                modelo.Id = Guid.NewGuid();
            }

            // TODO: Guardar en la base de datos de manera asíncrona
            // await _repository.InsertAsync(modelo);

            return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfiguracionDocumentacion>>> Get()
        {
            // TODO: Consultar masivamente usando LINQ
            // var lista = await _repository.GetAllAsync();

            List<ConfiguracionDocumentacion> listaSimulada = [];

            return Ok(listaSimulada);
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<ConfiguracionDocumentacion>> GetById([FromRoute] Guid id)
        {
            // TODO: Buscar por Id (string) en la BD
            // var configuracion = await _repository.GetByIdAsync(id);
            // if (configuracion == null) return NotFound($"No se encontró la configuración con ID: {id}");

            ConfiguracionDocumentacion modeloSimulado = new()
            {
                Id = id,
                Name = "Configuración Estándar Enterprise",
                Type = (char)TipoDocumentacion.JSON,
                ProyectoId = Guid.NewGuid(),
                Description = "Formato base para microservicios core",
                IsActive = true,
                BaseEndpoint = "https://api.enterprise.com/v1",
                ApiType = (char)TipoApi.REST
            };

            return Ok(modeloSimulado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ConfiguracionDocumentacion modelo)
        {
            if (id != modelo.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del modelo enviado.");

            // TODO: Actualizar la entidad completa en la base de datos
            // var actualizado = await _repository.UpdateAsync(modelo);
            // if (!actualizado) return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] ConfiguracionDocumentacion modeloEstado)
        {
            // TODO: Recuperar entidad de la BD, actualizar solo el campo de estado y guardar cambios
            // var modificado = await _repository.CambiarEstadoAsync(id, modeloEstado.Type);
            // if (!modificado) return NotFound();
            modeloEstado.UpdatedDate = DateTime.UtcNow;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // TODO: Lógica para validar relaciones en BD y proceder con Hard Delete o cascada
            // var eliminado = await _repository.DeletePhysicalAsync(id);
            // if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
