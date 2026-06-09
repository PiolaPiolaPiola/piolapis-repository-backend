using Microsoft.AspNetCore.Mvc;

namespace PiolAPIS_Repository.Models
{
    public class ConfiguracionDocumentacion : Base
    {
        public string BaseEndpoint { get; set; }
        public char ApiType { get; set; }
        public Guid ProyectoId { get; set; }
    }
}
