using Microsoft.AspNetCore.Mvc;

namespace PiolAPIS_Repository.Domain.Entities
{
    public class DocumentationSetting : Base
    {
        public string BaseEndpoint { get; set; }
        public char ApiType { get; set; }
        public Guid ProyectoId { get; set; }
    }
}
