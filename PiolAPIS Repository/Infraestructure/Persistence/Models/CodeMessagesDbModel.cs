using System.ComponentModel.DataAnnotations.Schema;

namespace PiolAPIS_Repository.Infraestructure.Persistence.Models
{
    [Table("CodeMessages")]
    public class CodeMessagesDbModel : BaseDbModel
    {
        public string HttpCode { get; set; }
        public string Response { get; set; }
        public char ResponseType { get; set; }
    }
}
