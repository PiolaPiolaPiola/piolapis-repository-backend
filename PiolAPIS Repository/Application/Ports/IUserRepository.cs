using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public interface IUserRepository
    {
        void Save(User user);
        IList<User> GetAll(string rol);
        User GetById(Guid id);
        void Update(User user);
        void UpdateStatus(Guid id, bool status);
    }
}
