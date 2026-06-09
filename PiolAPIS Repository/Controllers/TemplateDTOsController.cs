using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Domain.Entities.Enums;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/plantillas-dtos")]
    public class TemplateDTOsController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<TemplateDTOs>> Post([FromBody] TemplateDTOs modelo)
        {
            if (modelo == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            modelo.Id ??= Guid.NewGuid();
            modelo.CreatedDate = DateTime.UtcNow;
            modelo.UpdatedDate = DateTime.UtcNow;

            // TODO: Persistir en la base de datos
            // await _repository.InsertAsync(modelo);

            return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateDTOs>>> Get([FromQuery] char? language, [FromQuery] string? tag)
        {
            // TODO: Implementar búsqueda con LINQ eficiente usando los nuevos campos
            // IQueryable<PlantillasDTOs> query = _repository.AsQueryable().Where(p => p.IsShared);
            // if (language.HasValue) query = query.Where(p => p.LanguageType == language.Value);
            // if (!string.IsNullOrEmpty(tag)) query = query.Where(p => p.Tags.Contains(tag));
            // var lista = await query.ToListAsync();

            List<TemplateDTOs> listaSimulada = [];

            return Ok(listaSimulada);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TemplateDTOs>> GetById([FromRoute] Guid id)
        {
            // TODO: Buscar plantilla en base de datos por ID
            // var plantilla = await _repository.GetByIdAsync(id);
            // if (plantilla == null) return NotFound($"No se encontró la plantilla con ID: {id}");

            TemplateDTOs modeloSimulado = new()
            {
                Id = id,
                Name = "PagedResponseDTO",
                Description = "Estructura estándar global para respuestas paginadas en la organización.",
                RequestType = (char)RequestType.GET,
                Request = string.Empty,
                Response = "{ \"PageNumber\": 1, \"PageSize\": 10, \"TotalRecords\": 100, \"Data\": [] }",
                ResponseType = (char)DocumentationType.JSON,
                IsShared = true,
                Tags = "Pagination,Standard",
                IsActive = true,
                CreatedDate = DateTime.UtcNow.AddDays(-5),
                UpdatedDate = DateTime.UtcNow
            };

            return Ok(modeloSimulado);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] TemplateDTOs modelo)
        {
            if (id != modelo.Id)
                return BadRequest("El ID de la ruta no coincide con el ID de la plantilla provista.");

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

        [HttpPost("importar-estructura")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<TemplateDTOs>> ImportarEstructura([FromForm] TemplateDTOs modelo)
        {
            if (modelo.File == null || modelo.File.Length == 0)
                return BadRequest("Se debe adjuntar un archivo válido para extraer la estructura.");

            try
            {
                using var stream = modelo.File.OpenReadStream();
                using var reader = new StreamReader(stream);
                string contenidoArchivo = await reader.ReadToEndAsync();

                // var contratoExtraido = _contractParser.Parse(contenidoArchivo);

                modelo.RequestType = (char)RequestType.POST;
                modelo.Request = "{ \"id\": 0, \"nombre\": \"string\" }"; // Extraído del archivo
                modelo.Response = "{ \"status\": \"success\", \"data\": { \"id\": 123 } }"; // Extraído del archivo
                modelo.ResponseType = (char)DocumentationType.JSON;
                modelo.Name = Path.GetFileNameWithoutExtension(modelo.File.FileName);

                return Ok(modelo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al leer el contrato de la API: {ex.Message}");
            }
        }
    }
}
