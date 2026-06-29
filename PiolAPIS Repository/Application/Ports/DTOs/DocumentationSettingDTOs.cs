namespace PiolAPIS_Repository.Application.Ports.DTOs
{
    public class DocumentationSettingDTOs
    {
        public record CreateDocumentationSettingRequest(
            string Name,
            string Description,
            string Code,
            char? Type,
            string BaseEndpoint,
            char ApiType,
            Guid ProyectoId
        );

        public record UpdateDocumentationSettingRequest(
            string Name,
            string Description,
            string BaseEndpoint,
            char ApiType
        );

        public record ChangeDocumentationSettingStatusRequest(
            bool IsActive
        );
    }
}