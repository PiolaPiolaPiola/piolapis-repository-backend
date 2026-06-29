using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    [Table("projects")]
    public class ProjectsDbModel : BaseDbModel
    {
        public ProjectsDbModel() { }

        [Key]
        public Guid? Id { get; set; }
    }
}