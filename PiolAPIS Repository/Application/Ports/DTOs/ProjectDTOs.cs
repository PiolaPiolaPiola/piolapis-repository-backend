namespace PiolAPIS_Repository.Application.Ports.DTOs
{
    public class ProjectDTOs
    {
        public record CreateProjectRequest(
            string Name,
            string Description,
            string Code,
            char? Type
        );

        public record UpdateProjectRequest(
            string Name,
            string Description
        );

        public record ChangeProjectStatusRequest(
            bool IsActive
        );
    }
}
