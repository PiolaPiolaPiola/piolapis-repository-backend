using PiolAPIS_Repository.Domain.Entities;

namespace PiolAPIS_Repository.Application.Ports
{
    public interface ICodeMessageRepository
    {
        void Save(CodeMessage codeMessage);
        IList<CodeMessage> GetAll(string rol);
        CodeMessage GetById(Guid id);
        void Update(CodeMessage codeMessage);
        void UpdateStatus(Guid id, bool status);
    }
}
