using PiolAPIS_Repository.Domain.Entities;
namespace PiolAPIS_Repository.Application.Ports.User
{
    public class UpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Users> Execute(Users user)
        {
            return await _userRepository.UpdateAsync(user);
        }
    }
}