using System.ComponentModel.DataAnnotations;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    public class BaseDbModel
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]//TODO: Validar qué tamaño
        public string Description { get; set; } = string.Empty;
        public char? Type { get; set; }
        public string Code { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime? UpdatedDate { get; set; }
    }
}
