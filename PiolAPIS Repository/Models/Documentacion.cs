namespace PiolAPIS_Repository.Models
{
    public class Documentacion : Base
    {
        public Guid? Id { get; set; }

        public Guid ProyectoId { get; set; }
        public Guid ConfiguracionDocumentacionId { get; set; }
        public Guid PlantillaDtoId { get; set; }
        public string Version { get; set; } = "1.0.0";         
    }
}
