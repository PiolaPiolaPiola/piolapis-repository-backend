using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    [Table("variables")]
    public class VariablesDbModel : BaseDbModel
    {
        public VariablesDbModel() { }

        [Key]
        public Guid? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DataType { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ExampleValue { get; set; }
    }
}