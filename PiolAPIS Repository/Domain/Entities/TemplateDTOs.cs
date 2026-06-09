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
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
