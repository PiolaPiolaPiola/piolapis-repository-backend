using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public interface IVariableRepository
    {
        void Save(Variable variable);
        IList<Variable> GetAll();
        Variable GetById(Guid id);
        void Update(Variable variable);
        void UpdateStatus(Guid id, bool status);
    }
}
