namespace PiolAPIS_Repository.Domain.Entities
{
    public class CodeMessage : Base
    {
        public string HTTP_code { get; set; }

        public string Response { get; set; } //Estructura completa del code y el message

        public char ResponseType { get; set; } //Si es schema, JSON, etc

        public CodeMessage(Guid? id, string name, string description, char? type, string code, bool isActive, DateTime? createdDate, DateTime? updatedDate, string HTTP_code, string Response)
        {

        }
    }
}
