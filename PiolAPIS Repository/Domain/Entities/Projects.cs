namespace PiolAPIS_Repository.Domain.Entities
{
    public class Projects : Base
    {
        private readonly List<DocumentationSettings> _settings = [];
        public IReadOnlyCollection<DocumentationSettings> Settings => _settings.AsReadOnly();

        private readonly List<Documentations> _documentations = [];
        public IReadOnlyCollection<Documentations> Documentations => _documentations.AsReadOnly();

        protected Projects() : base() { }

        public Projects(
            Guid? id,
            string name,
            string description,
            char? type,
            string code,
            bool isActive,
            DateTime? createdDate,
            DateTime? updatedDate)
            : base(id, name, description, type, code, isActive, createdDate, updatedDate)
        {
        }

        public void UpdateProject(string newName, string newDescription)
        {
            UpdateMetadata(newName, newDescription, this.Code);
        }

        public void LinkDocumentationSetting(DocumentationSettings setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting), "La configuración de documentación no puede ser nula.");

            if (setting.ProyectoId != this.Id)
                throw new ArgumentException("La configuración no pertenece a este proyecto.");

            _settings.Add(setting);
        }

        public void LinkDocumentation(Documentations documentation)
        {
            if (documentation == null)
                throw new ArgumentNullException(nameof(documentation), "La documentación no puede ser nula.");

            _documentations.Add(documentation);
        }
    }
}