namespace PiolAPIS_Repository.Domain.Entities
{
    public class Project : Base
    {
        private readonly List<DocumentationSetting> _settings = [];
        public IReadOnlyCollection<DocumentationSetting> Settings => _settings.AsReadOnly();

        private readonly List<Documentation> _documentations = [];
        public IReadOnlyCollection<Documentation> Documentations => _documentations.AsReadOnly();

        protected Project() : base() { }

        public Project(
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

        public void LinkDocumentationSetting(DocumentationSetting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting), "La configuración de documentación no puede ser nula.");

            if (setting.ProyectoId != this.Id)
                throw new ArgumentException("La configuración no pertenece a este proyecto.");

            _settings.Add(setting);
        }

        public void LinkDocumentation(Documentation documentation)
        {
            if (documentation == null)
                throw new ArgumentNullException(nameof(documentation), "La documentación no puede ser nula.");

            _documentations.Add(documentation);
        }
    }
}
