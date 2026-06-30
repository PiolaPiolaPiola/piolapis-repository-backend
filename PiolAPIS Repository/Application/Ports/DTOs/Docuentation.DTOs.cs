using System;

namespace PiolAPIS_Repository.Application.Ports.DTOs
{
    public class DocumentationDTOs
    {
        public record CreateDocumentationRequest(
            string Name,
            string Description,
            string Version,
            Guid ProyectoId,
            Guid ConfiguracionDocumentacionId,
            Guid? PlantillaDtoIdRequest,
            Guid? PlantillaDtoResponse,
            string? EndpointEspecifico,
            string? Parametros,
            string? MensajesError,
            bool IsPublic
        );

        public record UpdateDocumentationRequest(
            string Name,
            string Description,
            string Version,
            Guid? PlantillaDtoIdRequest,
            Guid? PlantillaDtoResponse,
            string? EndpointEspecifico,
            string? Parametros,
            string? MensajesError,
            bool IsPublic
        );

        public record ChangeDocumentationStatusRequest(
            bool IsActive
        );
    }
}