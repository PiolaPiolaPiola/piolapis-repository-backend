using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    [Table("Users")]
    public class UsersDbModel : BaseDbModel
    {
        public UsersDbModel() { }

        [Key]
        public Guid? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = string.Empty;

        public string FullName => $"{Name} {LastName}".Trim();
    }
}
