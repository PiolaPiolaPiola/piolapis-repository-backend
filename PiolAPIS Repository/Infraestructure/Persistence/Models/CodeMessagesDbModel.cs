using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    [Table("codemessages")]
    public class CodeMessagesDbModel : BaseDbModel
    {
        public CodeMessagesDbModel() { }

        [Key]
        public Guid? Id { get; set; }

        [Required]
        [StringLength(3)]
        public string HttpCode { get; set; } = string.Empty;

        [Required]
        public string Response { get; set; } = string.Empty;

        [Required]
        public char ResponseType { get; set; }
    }
}