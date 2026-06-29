using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.Project;
using PiolAPIS_Repository.Application.Ports.DTOs;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly CreateProjectUseCase _createProjectUseCase;
        private readonly GetProjectByIdUseCase _getProjectByIdUseCase;
        private readonly GetAllProjectsUseCase _getAllProjectsUseCase;
        private readonly UpdateProjectUseCase _updateProjectUseCase;
        private readonly DeleteProjectUseCase _deleteProjectUseCase;

        private readonly IProjectRepository _projectRepository;

        public ProjectController(
            CreateProjectUseCase createProjectUseCase,
            GetProjectByIdUseCase getProjectByIdUseCase,
            GetAllProjectsUseCase getAllProjectsUseCase,
            UpdateProjectUseCase updateProjectUseCase,
            DeleteProjectUseCase deleteProjectUseCase,
            IProjectRepository projectRepository)
        {
            _createProjectUseCase = createProjectUseCase;
            _getProjectByIdUseCase = getProjectByIdUseCase;
            _getAllProjectsUseCase = getAllProjectsUseCase;
            _updateProjectUseCase = updateProjectUseCase;
            _deleteProjectUseCase = deleteProjectUseCase;
            _projectRepository = projectRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectDTOs.CreateProjectRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            var nuevoProyecto = new Projects(
                id: null,
                name: request.Name,
                description: request.Description,
                type: request.Type,
                code: request.Code,
                isActive: true,
                createdDate: null,
                updatedDate: null
            );

            var resultado = await _createProjectUseCase.Execute(nuevoProyecto);

            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projects>>> Get()
        {
            var proyectos = await _getAllProjectsUseCase.Execute();
            return Ok(proyectos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Projects>> GetById([FromRoute] Guid id)
        {
            try
            {
                var proyecto = await _getProjectByIdUseCase.Execute(id);
                return Ok(proyecto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ProjectDTOs.UpdateProjectRequest request)
        {
            try
            {
                var proyectoExistente = await _getProjectByIdUseCase.Execute(id);
                if (proyectoExistente == null)
                    return NotFound($"No se encontró el proyecto con ID: {id}");

                proyectoExistente.UpdateProject(request.Name, request.Description);

                await _updateProjectUseCase.Execute(proyectoExistente);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> PatchStatus([FromRoute] Guid id, [FromBody] ProjectDTOs.ChangeProjectStatusRequest request)
        {
            var modificado = await _projectRepository.ChangeStatusAsync(id, request.IsActive);
            if (!modificado)
                return NotFound($"No se pudo cambiar el estado. No existe el proyecto con ID: {id}");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _deleteProjectUseCase.Execute(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}