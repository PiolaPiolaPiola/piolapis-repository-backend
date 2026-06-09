using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public interface IProyectRepository
    {
        void Save(Proyect proyect);
        IList<Proyect> GetAll();
        Variable GetById(Guid id);
        void Update(Proyect proyect);
        void UpdateStatus(Guid id, bool status);
    }
}
