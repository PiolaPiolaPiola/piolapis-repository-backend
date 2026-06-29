using PiolAPIS_Repository.Domain.Entities;
namespace PiolAPIS_Repository.Application.Ports.User
{
    public class GetAllUsersByRoleUseCase
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersByRoleUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<Users>> Execute(string role)
        {
            return await _userRepository.GetAllByRoleAsync(role);
        }
    }
}
