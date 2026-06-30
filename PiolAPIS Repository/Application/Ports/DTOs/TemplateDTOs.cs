using System;

namespace PiolAPIS_Repository.Application.Ports.DTOs
{
    public static class TemplateDTOs
    {
        public record CreateTemplateRequest(
            string Name,
            string Description,
            char? Type,
            string Code,
            char RequestType,
            string? Request,
            string? Response,
            char ResponseType,
            bool IsShared,
            string? Tags
        );
        public record UpdateTemplateRequest(
            string Name,
            string Description,
            char? Type,
            string Code,
            char RequestType,
            string Request,
            string Response,
            char ResponseType,
            string? Tags
        );
    }
}