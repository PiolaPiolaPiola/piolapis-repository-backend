namespace PiolAPIS_Repository.Application.Ports.DTOs
{
    public class VariableDTOs
    {
        public record CreateVariableRequest(
            string Name,
            string Description,
            string Code,
            char? Type,
            string DataType,
            string? ExampleValue
        );

        public record UpdateVariableRequest(
            string Description,
            string DataType,
            string? ExampleValue
        );

        public record ChangeVariableStatusRequest(
            bool IsActive
        );
    }
}