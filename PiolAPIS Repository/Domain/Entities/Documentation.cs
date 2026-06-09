namespace PiolAPIS_Repository.Domain.Entities
{
    public class Documentation : Base
    {
        public Guid? Id { get; set; }

        public Guid ProyectoId { get; set; }
        public Guid ConfiguracionDocumentacionId { get; set; }
        public Guid PlantillaDtoId { get; set; }
        public string Version { get; set; } = "1.0.0";         
    }
}
