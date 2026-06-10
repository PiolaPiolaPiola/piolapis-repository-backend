namespace PiolAPIS_Repository.Domain.Entities
{
    public class Variable : Base
    {
        public string DataType { get; private set; } = string.Empty;
        public string? ExampleValue { get; private set; }

        protected Variable() : base() { }

        public Variable(
            Guid? id,
            string name,
            string description,
            char? type,
            string code,
            bool isActive,
            DateTime? createdDate,
            DateTime? updatedDate,
            string dataType,
            string? exampleValue)
            : base(id, name, description, type, code, isActive, createdDate, updatedDate)
        {
            ValidateDataType(dataType);

            DataType = dataType.Trim();
            ExampleValue = exampleValue?.Trim();
        }

        public void UpdateConfiguration(string newDataType, string? newExampleValue, string newDescription)
        {
            ValidateDataType(newDataType);

            UpdateMetadata(this.Name, newDescription, this.Code);

            DataType = newDataType.Trim();
            ExampleValue = newExampleValue?.Trim();
        }

        private static void ValidateDataType(string dataType)
        {
            if (string.IsNullOrWhiteSpace(dataType))
                throw new ArgumentException("El tipo de dato es obligatorio.");

            string cleanedType = dataType.Trim();

            bool esValido = cleanedType == Entities.Enums.DataType.String ||
                            cleanedType == Entities.Enums.DataType.Int ||
                            cleanedType == Entities.Enums.DataType.Bool;

            if (!esValido)
            {
                throw new ArgumentException($"El tipo de dato '{dataType}' no está permitido. Debe ser {Entities.Enums.DataType.String}, {Entities.Enums.DataType.Int} o {Entities.Enums.DataType.Bool}.");
            }
        }
    }
}
