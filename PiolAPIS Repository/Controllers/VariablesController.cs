using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Application.Ports.Variable;
using PiolAPIS_Repository.Application.Ports.DTOs;

namespace PiolAPIS_Repository.Controllers
{
    [ApiController]
    [Route("api/v1/variables")]
    public class VariablesController : ControllerBase
    {
        private readonly CreateVariableUseCase _createVariableUseCase;
        private readonly GetVariableByIdUseCase _getVariableByIdUseCase;
        private readonly GetAllVariablesUseCase _getAllVariablesUseCase;
        private readonly UpdateVariableUseCase _updateVariableUseCase;
        private readonly DeleteVariableUseCase _deleteVariableUseCase;
        private readonly ChangeVariableStatusUseCase _changeVariableStatusUseCase;

        public VariablesController(
            CreateVariableUseCase createVariableUseCase,
            GetVariableByIdUseCase getVariableByIdUseCase,
            GetAllVariablesUseCase getAllVariablesUseCase,
            UpdateVariableUseCase updateVariableUseCase,
            DeleteVariableUseCase deleteVariableUseCase,
            ChangeVariableStatusUseCase changeVariableStatusUseCase)
        {
            _createVariableUseCase = createVariableUseCase;
            _getVariableByIdUseCase = getVariableByIdUseCase;
            _getAllVariablesUseCase = getAllVariablesUseCase;
            _updateVariableUseCase = updateVariableUseCase;
            _deleteVariableUseCase = deleteVariableUseCase;
            _changeVariableStatusUseCase = changeVariableStatusUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VariableDTOs.CreateVariableRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición no puede ser nulo.");

            var nuevaVariable = new Variables(
                id: null,
                name: request.Name,
                description: request.Description,
                type: null,
                code: null,
                isActive: true,
                createdDate: null,
                updatedDate: null,
                dataType: request.DataType,
                exampleValue: request.ExampleValue
            );

            await _createVariableUseCase.Execute(nuevaVariable);

            return CreatedAtAction(nameof(GetById), new { id = nuevaVariable.Id }, nuevaVariable);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Variables>>> Get([FromQuery] bool? includeInactive = false)
        {
            var variables = await _getAllVariablesUseCase.Execute();

            if (!includeInactive.Value)
            {
                variables = variables.Where(v => v.IsActive);
            }

            return Ok(variables);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Variables>> GetById([FromRoute] Guid id)
        {
            try
            {
                var variable = await _getVariableByIdUseCase.Execute(id);
                return Ok(variable);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] VariableDTOs.UpdateVariableRequest request)
        {
            try
            {
                var variableExistente = await _getVariableByIdUseCase.Execute(id);
                if (variableExistente == null)
                    return NotFound($"No se encontró la variable con ID: {id}");

                variableExistente.UpdateVariable(request.DataType, request.ExampleValue, request.Description);

                await _updateVariableUseCase.Execute(variableExistente);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<IActionResult> PatchEstado([FromRoute] Guid id, [FromBody] VariableDTOs.ChangeVariableStatusRequest request)
        {
            var modificado = await _changeVariableStatusUseCase.Execute(id, request.IsActive);
            if (!modificado)
                return NotFound($"No se pudo cambiar el estado. No existe la variable con ID: {id}");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _deleteVariableUseCase.Execute(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}