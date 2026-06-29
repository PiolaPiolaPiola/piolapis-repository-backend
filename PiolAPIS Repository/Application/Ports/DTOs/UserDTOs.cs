namespace PiolAPIS_Repository.Application.Ports.DTOs
{
    public class UserDTOs
    {
        public record CreateUserRequest(
            string Name, 
            string LastName, 
            string Description, 
            string Role, 
            string Code, 
            char? Type
        );

        public record UpdateUserRequest(
            string Name, 
            string LastName, 
            string Description
        );

        public record ChangeStatusRequest(
            bool IsActive
        );
    }
}
