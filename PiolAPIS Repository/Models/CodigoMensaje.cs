namespace PiolAPIS_Repository.Models
{
    public class CodigoMensaje : Base
    {
        public string HTTP_code { get; set; }

        public string Response { get; set; } //Estructura completa del code y el message

        public char ResponseType { get; set; } //Si es schema, JSON, etc
    }
}
