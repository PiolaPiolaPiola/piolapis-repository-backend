using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using System.Reflection;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/usuarios")]
    public class UserController : Controller
    {
        [HttpPost("registro")]
        public async Task<ActionResult<User>> Post([FromBody] User modelo)
        {
            if (modelo == null)
                return BadRequest("Se debe enviar un cuerpo en la petición");

            modelo.Id = Guid.NewGuid();
            modelo.CreatedDate = DateTime.UtcNow;
            modelo.UpdatedDate = DateTime.UtcNow;

            // TODO: Encriptar contraseña y guardar en la base de datos

            return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User modelo)
        {
            // TODO: Validar credenciales contra la BD y generar el JWT Token.
            // var token = await _authService.GenerateTokenAsync(modelo);

            return Ok(new { token = "Token-JWT-Simulado-XYZ..." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // TODO: Invalidar el token JWT actual (por ejemplo, añadiéndolo a una lista negra en Redis)
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get([FromQuery] string? rol)
        {
            List<User> usuariosSimulados = [];

            return Ok(usuariosSimulados);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetById([FromRoute] Guid id)
        {
            // TODO: Buscar el usuario por su GUID en la BD
            // var usuario = await _userRepository.GetByIdAsync(id);
            // if (usuario == null) return NotFound();

            User usuarioSimulado = new()
            {
                Id = id,
                Name = "Usuario",
                LastName = "Prueba",
                Role = "Admin"
            };

            return Ok(usuarioSimulado);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] User modelo)
        {
            if (id != modelo.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del modelo enviado.");

            // TODO: Obtener entidad de la BD, mapear los cambios permitidos y actualizar
            modelo.UpdatedDate = DateTime.UtcNow;
            // await _userRepository.UpdateAsync(modelo);

            return NoContent();
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] User modeloEstado)
        {
            modeloEstado.UpdatedDate = DateTime.UtcNow;
            // TODO: Implementar borrado lógico (e.g., Update de columna IsActive / Estado)
            // var modificado = await _userRepository.InactivarUsuarioAsync(id, modeloEstado.Role /* o una prop de estado */);
            // if (!modificado) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // TODO: Eliminación física (Hard Delete) de la base de datos
            // var eliminado = await _userRepository.DeletePhysicalAsync(id);
            // if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
