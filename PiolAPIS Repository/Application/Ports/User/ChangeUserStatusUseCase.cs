using PiolAPIS_Repository.Domain.Entities;
namespace PiolAPIS_Repository.Application.Ports.User
{
    public class ChangeUserStatusUseCase
    {
        private readonly IUserRepository _userRepository;
        public ChangeUserStatusUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Execute(Guid id, bool isActive)
        {
            return await _userRepository.ChangeStatusAsync(id, isActive);
        }
    }
}
