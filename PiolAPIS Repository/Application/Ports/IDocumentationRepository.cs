using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public interface IDocumentationRepository
    {
        void Save(Documentation documentation);
        IList<Documentation> GetAll();
        Documentation GetById(Guid id);
        void Update(Documentation documentation);
        void UpdateStatus(Guid id, bool status);
    }
}
