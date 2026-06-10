namespace PiolAPIS_Repository.Domain.Entities
{
    public class CodeMessage : Base
    {
        public string HttpCode { get; private set; } = string.Empty;
        public string Response { get; private set; } = string.Empty;
        public char ResponseType { get; private set; }

        protected CodeMessage() : base() { }

        public CodeMessage(
            Guid? id,
            string name,
            string description,
            char? type,
            string code,
            bool isActive,
            DateTime? createdDate,
            DateTime? updatedDate,
            string httpCode,
            string response,
            char responseType)
            : base(id, name, description, type, code, isActive, createdDate, updatedDate)
        {
            ValidateHttpCode(httpCode);
            ValidateResponse(response);

            HttpCode = httpCode.Trim();
            Response = response.Trim();
            ResponseType = responseType;
        }

        public void UpdateContract(string httpCode, string response, char responseType)
        {
            ValidateHttpCode(httpCode);
            ValidateResponse(response);

            HttpCode = httpCode.Trim();
            Response = response.Trim();
            ResponseType = responseType;

        }

        private static void ValidateHttpCode(string httpCode)
        {
            if (string.IsNullOrWhiteSpace(httpCode))
                throw new ArgumentException("El código HTTP es obligatorio.");

            if (httpCode.Length != 3 || !int.TryParse(httpCode, out _))
                throw new ArgumentException("El código HTTP debe ser un número válido de 3 dígitos (ej: 200, 404, 500).");
        }

        private static void ValidateResponse(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
                throw new ArgumentException("La estructura de la respuesta (Response) no puede estar vacía.");
        }
    }
}