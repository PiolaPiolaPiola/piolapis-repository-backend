using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    [Table("templatesdtos")]
    public class TemplatesDTOsDbModel : BaseDbModel
    {
        public TemplatesDTOsDbModel() { }

        [Key]
        public Guid? Id { get; set; }

        [Required]
        public char RequestType { get; set; }

        [Required]
        public string Request { get; set; } = string.Empty;

        [Required]
        public string Response { get; set; } = string.Empty;

        [Required]
        public char ResponseType { get; set; }

        [Required]
        public bool IsShared { get; set; }

        [StringLength(500)]
        public string? Tags { get; set; }
    }
}