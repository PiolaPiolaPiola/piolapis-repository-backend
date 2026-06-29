using System;
using PiolAPIS_Repository.Domain.Entities.Enums;

namespace PiolAPIS_Repository.Domain.Entities
{
    public class TemplatesDTOs : Base
    {
        public char RequestType { get; private set; }
        public string Request { get; private set; } = string.Empty;
        public string Response { get; private set; } = string.Empty;
        public char ResponseType { get; private set; }
        public bool IsShared { get; private set; }
        public string? Tags { get; private set; }

        protected TemplatesDTOs() : base() { }

        public TemplatesDTOs(
            Guid? id,
            string name,
            string description,
            char? type,
            string code,
            bool isActive,
            DateTime? createdDate,
            DateTime? updatedDate,
            char requestType,
            string request,
            string response,
            char responseType,
            bool isShared,
            string? tags)
            : base(id, name, description, type, code, isActive, createdDate, updatedDate)
        {
            ValidateRequestType(requestType);
            ValidateResponseType(responseType);

            RequestType = char.ToUpperInvariant(requestType);
            Request = request?.Trim() ?? string.Empty;
            Response = response?.Trim() ?? string.Empty;
            ResponseType = char.ToUpperInvariant(responseType);
            IsShared = isShared;
            Tags = tags?.Trim();
        }

        public void LoadContracts(char requestType, string request, string response, char responseType)
        {
            ValidateRequestType(requestType);
            ValidateResponseType(responseType);

            RequestType = char.ToUpperInvariant(requestType);
            Request = request.Trim();
            Response = response.Trim();
            ResponseType = char.ToUpperInvariant(responseType);
            UpdatedDate = DateTime.UtcNow;
        }

        public void UpdateSharing(bool isShared, string? newTags)
        {
            IsShared = isShared;
            Tags = newTags?.Trim();
            UpdatedDate = DateTime.UtcNow;
        }

        private static void ValidateRequestType(char requestType)
        {
            if (!Enum.IsDefined(typeof(RequestType), (int)requestType))
            {
                throw new ArgumentException($"El código de petición '{requestType}' no es válido en el sistema.");
            }
        }

        private static void ValidateResponseType(char responseType)
        {
            char upperChar = char.ToUpperInvariant(responseType);
            if (!Enum.IsDefined(typeof(DocumentationType), (int)upperChar))
            {
                throw new ArgumentException($"El tipo de respuesta '{responseType}' no está soportado.");
            }
        }
    }
}