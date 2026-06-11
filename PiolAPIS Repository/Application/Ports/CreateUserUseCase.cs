using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Ejecutar(User modelo)
        {
            bool userExist = await _userRepository.GetByIdAsync(modelo.Id.Value);

            if (userExist) throw new Exception("El usuario ya existe");
            _userRepository.SaveAsync(usuario);
        }
    }
}
