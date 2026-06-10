namespace PiolAPIS_Repository.Domain.Entities
{
    public abstract class Base 
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public char? Type { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; private set; }

        protected Base() { }

        public Base(Guid? id, string name, string description, char? type, string code, bool isActive, DateTime? createdDate, DateTime? updatedDate)
        {
            Id = id ?? Guid.NewGuid();

            ValidateName(name);
            ValidateDescription(description);

            Name = name.Trim();
            Description = description.Trim();
            Type = type;
            Code = code ?? string.Empty;
            IsActive = isActive;
            CreatedDate = createdDate ?? DateTime.UtcNow;
            UpdatedDate = updatedDate;
        }

        public void UpdateMetadata(string name, string description, string code)
        {
            ValidateName(name);
            ValidateDescription(description);

            Name = name.Trim();
            Description = description.Trim();
            Code = code;
            UpdatedDate = DateTime.UtcNow; 
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedDate = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedDate = DateTime.UtcNow;
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es obligatorio.");

            if (name.Length > 100)
                throw new ArgumentException("El nombre debe tener máximo 100 caracteres.");
        }

        private static void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("La descripción es obligatoria.");
        }
    }
}
