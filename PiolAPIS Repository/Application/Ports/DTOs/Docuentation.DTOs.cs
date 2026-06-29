namespace PiolAPIS_Repository.Application.Ports.DTOs
{
    public class DocumentationDTOs
    {
        public record CreateDocumentationRequest(
            string Name,
            string Description,
            string Code,
            char? Type,
            Guid ProyectoId,
            Guid ConfiguracionDocumentacionId,
            Guid PlantillaDtoId,
            string Version
        );

        public record UpdateDocumentationRequest(
            string Name,
            string Description,
            string Version,
            Guid PlantillaDtoId
        );

        public record ChangeDocumentationStatusRequest(
            bool IsActive
        );
    }
}