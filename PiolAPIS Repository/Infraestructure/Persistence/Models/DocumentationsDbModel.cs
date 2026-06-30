using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    [Table("documentations")]
    public class DocumentationsDbModel : BaseDbModel
    {
        public DocumentationsDbModel() { }

        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid ProyectoId { get; set; }

        [Required]
        public Guid ConfiguracionDocumentacionId { get; set; }

        public Guid? PlantillaDtoIdRequest { get; set; }
        public Guid? PlantillaDtoResponse { get; set; }

        [Required]
        [StringLength(20)]
        public string Version { get; set; } = "1.0.0";

        public string? EndpointEspecifico { get; set; }

        public string? Parametros { get; set; }

        public string? MensajesError { get; set; }

        public bool IsPublic { get; set; }
    }
}