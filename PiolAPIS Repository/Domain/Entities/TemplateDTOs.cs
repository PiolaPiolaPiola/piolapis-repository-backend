using PiolAPIS_Repository.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Domain.Entities
{
    public class TemplateDTOs : Base
    {
        public char RequestType { get; set; } //Si es Post, Put, etc
        public string Request { get; set; } //Estructura completa con las propiedades para peticiones POST
        public string Response { get; set; } //Estructura completa con las propiedades
        public char ResponseType { get; set; } //Si es schema, JSON, etc
        public bool IsShared { get; set; } //Indica si se podrà implementar en otras o no
        public string? Tags { get; set; } //Palabras clave para filtrar

        protected TemplateDTOs() : base() { }

        public TemplateDTOs(
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
        }

        public void UpdateSharing(bool isShared, string? newTags)
        {
            IsShared = isShared;
            Tags = newTags?.Trim();
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
