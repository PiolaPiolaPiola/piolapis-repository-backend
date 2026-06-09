namespace PiolAPIS_Repository.Domain.Entities
{
    public class Variable :Base
    {
        public string DataType { get; set; } 
        public string? ExampleValue { get; set; }               
    }
}
