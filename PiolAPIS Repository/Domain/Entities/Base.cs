namespace PiolAPIS_Repository.Domain.Entities
{
    public class Base
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char? Type { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Base()
        {

        }

        public Base(Guid? id, string name, string description, char? type, string code, bool isActive, DateTime? createdDate, DateTime? updatedDate)
        {
            Id = id ?? Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(name) || name.Length <= 0)
            {
                throw new ArgumentNullException("El nombre es obligatorio");
            }

            if (name.Length > 100)
            {
                throw new ArgumentException("El nombre debe tener máximo 100 caracteres");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("La descripción es obligatoria");
            }

            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
            Type = type;
            Code = code;
            IsActive = isActive;
            CreatedDate = createdDate ?? DateTime.UtcNow;
            UpdatedDate = updatedDate ?? DateTime.UtcNow;
        }
    }
}
