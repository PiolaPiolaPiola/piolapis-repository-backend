using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Application.Ports.User;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.DTOs;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly GetUserByIdUseCase _getUserByIdUseCase;
        private readonly GetAllUsersByRoleUseCase _getAllUsersByRoleUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase;
        private readonly DeleteUserUseCase _deleteUserUseCase;
        private readonly ChangeUserStatusUseCase _changeUserStatusUseCase;
        private readonly GetAllUsersUseCase _getAllUsersUseCase;

        public UserController(
            CreateUserUseCase createUserUseCase,
            GetUserByIdUseCase getUserByIdUseCase,
            GetAllUsersByRoleUseCase getAllUsersByRoleUseCase,
            UpdateUserUseCase updateUserUseCase,
            DeleteUserUseCase deleteUserUseCase,
            ChangeUserStatusUseCase changeUserStatusUseCase,
            GetAllUsersUseCase getAllUsersUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _getAllUsersByRoleUseCase = getAllUsersByRoleUseCase;
            _updateUserUseCase = updateUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _changeUserStatusUseCase = changeUserStatusUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTOs.CreateUserRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición no puede estar vacío");

            var nuevoUsuario = new Users(
                id: null,
                name: request.Name,
                description: request.Description,
                type: null,
                code: null,
                isActive: true,
                createdDate: null,
                updatedDate: null,
                lastName: request.LastName,
                role: request.Role
            );

            await _createUserUseCase.Execute(nuevoUsuario);

            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.Id }, nuevoUsuario);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Users>> GetById([FromRoute] Guid id)
        {
            try
            {
                var usuario = await _getUserByIdUseCase.Execute(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UserDTOs.UpdateUserRequest request)
        {
            var usuarioExistente = await _getUserByIdUseCase.Execute(id);
            if (usuarioExistente == null)
                return NotFound($"No se encontró el usuario con ID: {id}");

            usuarioExistente.UpdateProfile(request.Name, request.LastName, request.Description);

            await _updateUserUseCase.Execute(usuarioExistente);

            return NoContent();
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] UserDTOs.ChangeStatusRequest request)
        {
            var modificado = await _changeUserStatusUseCase.Execute(id, request.IsActive);
            if (!modificado)
                return NotFound($"No se pudo cambiar el estado. No existe el usuario con ID: {id}");

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> Get([FromQuery] string? rol = null, bool? includeInactive = false)
        {
            IEnumerable<Users> usuarios = new List<Users>();

            if (string.IsNullOrWhiteSpace(rol))
            {
                usuarios = await _getAllUsersUseCase.Execute();
            }
            else
            {
                usuarios = await _getAllUsersByRoleUseCase.Execute(rol);
            }

            if (!includeInactive.Value)
            {
                usuarios = usuarios.Where(u => u.IsActive);
            }

            return Ok(usuarios);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var usuarioExistente = await _getUserByIdUseCase.Execute(id);

            if (usuarioExistente == null)
                return NotFound($"No se encontró el usuario con ID: {id}");

            await _deleteUserUseCase.Execute(id);
            return NoContent();
        }
    }
}