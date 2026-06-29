using System;

namespace PiolAPIS_Repository.Domain.Entities
{
    public class Documentations : Base
    {
        public Guid ProyectoId { get; private set; }
        public Guid ConfiguracionDocumentacionId { get; private set; }
        public Guid PlantillaDtoId { get; private set; }
        public string Version { get; private set; } = "1.0.0";

        protected Documentations() : base() { }

        public Documentations(
            Guid? id,
            string name,
            string description,
            char? type,
            string code,
            bool isActive,
            DateTime? createdDate,
            DateTime? updatedDate,
            Guid proyectoId,
            Guid configuracionDocumentacionId,
            Guid plantillaDtoId,
            string version)
            : base(id, name, description, type, code, isActive, createdDate, updatedDate)
        {
            if (proyectoId == Guid.Empty)
                throw new ArgumentException("El ProyectoId proporcionado no es válido.");

            if (configuracionDocumentacionId == Guid.Empty)
                throw new ArgumentException("El ConfiguracionDocumentacionId proporcionado no es válido.");

            if (plantillaDtoId == Guid.Empty)
                throw new ArgumentException("El PlantillaDtoId no es válido.");

            ValidateVersion(version);

            ProyectoId = proyectoId;
            ConfiguracionDocumentacionId = configuracionDocumentacionId;
            PlantillaDtoId = plantillaDtoId;
            Version = string.IsNullOrWhiteSpace(version) ? "1.0.0" : version.Trim();
        }

        public void UpdateDocumentation(string newName, string newLastName, string newVersion, Guid newPlantillaDtoId)
        {
            UpdateMetadata(newName, newLastName, this.Code);
            UpdateVersion(newVersion);
            ChangePlantilla(newPlantillaDtoId);
        }

        public void UpdateVersion(string newVersion)
        {
            ValidateVersion(newVersion);
            Version = newVersion.Trim();
        }

        public void ChangePlantilla(Guid newPlantillaDtoId)
        {
            if (newPlantillaDtoId == Guid.Empty)
                throw new ArgumentException("La nueva plantilla no es válida.");

            PlantillaDtoId = newPlantillaDtoId;
        }

        private static void ValidateVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentException("El campo de versión es obligatorio.");
        }

        public Tuple<Guid, Guid, Guid> GetRelations() => Tuple.Create(ProyectoId, ConfiguracionDocumentacionId, PlantillaDtoId);
    }
}