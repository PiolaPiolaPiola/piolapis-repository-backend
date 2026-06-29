using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    [Table("documentationsettings")]
    public class DocumentationSettingsDbModel : BaseDbModel
    {
        public DocumentationSettingsDbModel() { }

        [Key]
        public Guid? Id { get; set; }

        [Required]
        public string BaseEndpoint { get; set; } = string.Empty;

        [Required]
        public char ApiType { get; set; }

        [Required]
        public Guid ProyectoId { get; set; }
    }
}