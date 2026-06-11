using Microsoft.AspNetCore.Mvc;

namespace PiolAPIS_Repository.Domain.Entities
{
    public class DocumentationSetting : Base
    {
        public string BaseEndpoint { get; private set; } = string.Empty;
        public char ApiType { get; private set; } 
        public Guid ProyectoId { get; private set; }

        protected DocumentationSetting() : base() { }

        public DocumentationSetting(
            Guid? id,
            string name,
            string description,
            bool isActive,
            DateTime? createdDate,
            DateTime? updatedDate,
            string baseEndpoint,
            char apiType,
            Guid proyectoId)
        {
            if (proyectoId == Guid.Empty)
                throw new ArgumentException("El ProyectoId debe ser válido.");

            ValidateBaseEndpoint(baseEndpoint);
            ValidateApiType(apiType);

            BaseEndpoint = baseEndpoint.Trim();
            ApiType = char.ToUpperInvariant(apiType);
            ProyectoId = proyectoId;
        }

        public void UpdateEndpointConfiguration(string newBaseEndpoint, char newApiType)
        {
            ValidateBaseEndpoint(newBaseEndpoint);
            ValidateApiType(newApiType);

            BaseEndpoint = newBaseEndpoint.Trim();
            ApiType = char.ToUpperInvariant(newApiType);
        }

        private static void ValidateBaseEndpoint(string baseEndpoint)
        {
            if (string.IsNullOrWhiteSpace(baseEndpoint))
                throw new ArgumentException("El endpoint base (BaseEndpoint) es obligatorio.");

            if (!Uri.TryCreate(baseEndpoint, UriKind.Absolute, out _))
                throw new ArgumentException("El endpoint base no tiene un formato de URL válido.");
        }

        private static void ValidateApiType(char apiType)
        {

        }
    }
}
