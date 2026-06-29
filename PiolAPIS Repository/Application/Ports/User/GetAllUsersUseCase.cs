using PiolAPIS_Repository.Domain.Entities;
namespace PiolAPIS_Repository.Application.Ports.User
{
    public class GetAllUsersUseCase
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<Users>> Execute()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
