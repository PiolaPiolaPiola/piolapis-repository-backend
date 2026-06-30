namespace PiolAPIS_Repository.Application.Ports.DTOs
{
    public class CodeMessageDTOs
    {
        public record CreateCodeMessageRequest(
            string Name,
            string Description,
            string HttpCode,
            string Response,
            char ResponseType
        );

        public record UpdateCodeMessageRequest(
            string Name,
            string Description,
            string HttpCode,
            string Response,
            char ResponseType
        );

        public record ChangeCodeMessageStatusRequest(
            bool IsActive
        );
    }
}