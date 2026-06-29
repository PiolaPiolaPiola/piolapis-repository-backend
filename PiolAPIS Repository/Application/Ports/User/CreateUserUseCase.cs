using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports.User
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(Users modelo)
        {
            var userExist = await _userRepository.GetByIdAsync(modelo.Id.Value);

            if (userExist != null) throw new Exception("El usuario ya existe");
            await _userRepository.SaveAsync(modelo);
        }
    }
}
