using System;

namespace PiolAPIS_Repository.Domain.Entities
{
    public class User : Base
    {
        public string LastName { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty; 

        public string FullName => $"{Name} {LastName}".Trim();

        protected User() : base() { }

        public User(
            Guid? id,
            string name, 
            string description,
            char? type,
            string code,
            bool isActive,
            DateTime? createdDate,
            DateTime? updatedDate,
            string lastName,
            string role)
            : base(id, name, description, type, code, isActive, createdDate, updatedDate)
        {
            ValidateLastName(lastName);
            ValidateRole(role);

            LastName = lastName.Trim();
            Role = role.Trim();
        }

        public void UpdateProfile(string newName, string newLastName, string newDescription)
        {
            ValidateLastName(newLastName);

            UpdateMetadata(newName, newDescription, this.Code);

            LastName = newLastName.Trim();
        }

        public void ChangeRole(string newRole)
        {
            ValidateRole(newRole);
            Role = newRole.Trim();
        }

        private static void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("El apellido es obligatorio.");

            if (lastName.Length > 100)
                throw new ArgumentException("El apellido no puede superar los 100 caracteres.");
        }

        private static void ValidateRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("El rol es obligatorio.");
        }
    }
}
