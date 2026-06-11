using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Controllers
{
    public class ProjectController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<Project>> Post([FromBody] Project modelo)
        {
            if (modelo == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            modelo.Id ??= Guid.NewGuid();
            modelo.CreatedDate = DateTime.UtcNow;
            modelo.UpdatedDate = DateTime.UtcNow;

            // TODO: Guardar en la BD
            // await _proyectoRepository.InsertAsync(modelo);

            return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            // TODO: Consultar proyectos que no estén eliminados lógicamente
            // var proyectos = await _proyectoRepository.GetActivosAsync();

            List<Project> listaSimulada = [];

            return Ok(listaSimulada);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Project>> GetById([FromRoute] Guid id)
        {
            // TODO: Buscar el proyecto por su ID e incluir el árbol de APIs vinculadas (.Include(p => p.Apis) con EF Core)
            // var proyecto = await _proyectoRepository.GetByIdWithApisAsync(id);
            // if (proyecto == null) return NotFound($"No se encontró el proyecto con ID: {id}");

            Project modeloSimulado = new Project(
                    id: id,
                    name: "Proyecto Core E-Commerce",
                    description: "Contenedor principal para las APIs de Checkout, Catálogo y Pagos.",
                    isActive: true,
                    createdDate: DateTime.UtcNow.AddDays(-10),
                    updatedDate: DateTime.UtcNow
                );

            return Ok(modeloSimulado);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] Project modelo)
        {
            if (id != modelo.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del proyecto enviado.");

            // TODO: Recuperar entidad actual, mapear Name, Description, Code y actualizar fecha
            modelo.UpdatedDate = DateTime.UtcNow;

            // var actualizado = await _proyectoRepository.UpdateAsync(modelo);
            // if (!actualizado) return NotFound();

            return NoContent(); 
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // TODO: Implementar Soft Delete (Borrado Lógico) en el repositorio
            // Al ser borrado lógico, no hacemos un 'Remove' físico. 
            // Cambiamos un flag (ej. IsDeleted = true) y actualizamos la propiedad 'UpdatedDate'.

            // var borradoExitoso = await _proyectoRepository.SoftDeleteAsync(id);
            // if (!borradoExitoso) return NotFound();

            return NoContent(); // Se retorna 204 NoContent indicando que el recurso se procesó correctamente
        }
    }
}
