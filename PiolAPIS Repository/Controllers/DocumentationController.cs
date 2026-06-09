using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/documentaciones")]
    public class DocumentationController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<Documentation>> Post([FromBody] Documentation modelo)
        {
            if (modelo == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            modelo.Id ??= Guid.NewGuid();
            modelo.IsActive = true;
            modelo.CreatedDate = DateTime.UtcNow;
            modelo.UpdatedDate = DateTime.UtcNow;

            // TODO: Validar que existan previamente el ProyectoId, ConfiguracionId y PlantillaId en BD
            // await _repository.InsertAsync(modelo);

            return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documentation>>> Get([FromQuery] Guid? proyectoId)
        {
            // TODO: Implementar búsqueda con LINQ eficiente
            // IQueryable<Documentacion> query = _repository.AsQueryable();
            // if (proyectoId.HasValue) query = query.Where(d => d.ProyectoId == proyectoId.Value);
            // var lista = await query.ToListAsync();

            List<Documentation> listaSimulada = [];

            return Ok(listaSimulada);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Documentation>> GetById([FromRoute] Guid id)
        {
            // var documentacion = await _repository.GetByIdCompleteAsync(id);
            // if (documentacion == null) return NotFound($"No existe documentación con el ID: {id}");

            Documentation modeloSimulado = new()
            {
                Id = id,
                ProyectoId = Guid.NewGuid(),
                ConfiguracionDocumentacionId = Guid.NewGuid(),
                PlantillaDtoId = Guid.NewGuid(),
                Name = "V1/Checkout/Process-Payment",
                Description = "Documentación del contrato base para la integración con pasarelas de pago.",
                Version = "1.0.4",
                IsActive = true, 
                CreatedDate = DateTime.UtcNow.AddMonths(-1),
                UpdatedDate = DateTime.UtcNow
            };

            return Ok(modeloSimulado);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] Documentation modelo)
        {
            if (id != modelo.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la petición.");

            modelo.UpdatedDate = DateTime.UtcNow;

            // var actualizado = await _repository.UpdateAsync(modelo);
            // if (!actualizado) return NotFound();

            return NoContent(); 
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] Documentation modeloEstado)
        {
            // var modificado = await _repository.CambiarStatusAsync(id, modeloEstado.Status);
            // if (!modificado) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // TODO: Eliminar físicamente el registro de la tabla
            // var eliminado = await _repository.DeletePhysicalAsync(id);
            // if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
