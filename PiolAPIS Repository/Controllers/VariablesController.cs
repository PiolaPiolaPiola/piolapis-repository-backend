using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Domain.Entities.Enums;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/variables")]
    public class VariablesController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<Variable>> Post([FromBody] Variable modelo)
        {
            if (modelo == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(modelo.Name))
                return BadRequest("El nombre de la variable es obligatorio.");

            modelo.Id ??= Guid.NewGuid();
            modelo.IsActive = true; // Por defecto nace activa
            modelo.CreatedDate = DateTime.UtcNow;
            modelo.UpdatedDate = DateTime.UtcNow;

            // TODO: Persistir en la base de datos de forma asíncrona
            // await _repository.InsertAsync(modelo);

            return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Variable>>> Get([FromQuery] bool? soloActivas)
        {
            // TODO: Consultar base de datos aplicando filtros con LINQ expresiones
            // IQueryable<Variable> query = _repository.AsQueryable();
            // if (soloActivas.HasValue) query = query.Where(v => v.IsActive == soloActivas.Value);
            // var variables = await query.ToListAsync();

            List<Variable> listaSimulada = [];

            return Ok(listaSimulada);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Variable>> GetById([FromRoute] Guid id)
        {
            // var variable = await _repository.GetByIdAsync(id);
            // if (variable == null) return NotFound($"No se encontró la variable con ID: {id}");

            Variable modeloSimulado = new()
            {
                Id = id,
                Name = "Code",
                Description = "Código alfanumérico estandarizado para identificar registros del sistema.",
                DataType = DataType.String,
                ExampleValue = "DOC-2026-XYZ",
                IsActive = true,
                CreatedDate = DateTime.UtcNow.AddMonths(-2),
                UpdatedDate = DateTime.UtcNow
            };

            return Ok(modeloSimulado);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] Variable modelo)
        {
            if (id != modelo.Id)
                return BadRequest("El ID de la ruta no coincide con el ID de la variable provista.");

            // TODO: Actualizar la estructura completa en la base de datos
            modelo.UpdatedDate = DateTime.UtcNow;
            // var actualizado = await _repository.UpdateAsync(modelo);
            // if (!actualizado) return NotFound();

            return NoContent(); // 204 No Content para mutaciones completas exitosas
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] Variable modeloEstado)
        {
            // var modificado = await _repository.CambiarEstadoAsync(id, modeloEstado.IsActive);
            // if (!modificado) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // var eliminado = await _repository.DeletePhysicalIfUnusedAsync(id);
            // if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
