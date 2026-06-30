using System;

namespace PiolAPIS_Repository.Domain.Entities
{
    public class Documentations : Base
    {
        public Guid ProyectoId { get; private set; }
        public Guid ConfiguracionDocumentacionId { get; private set; }
        public Guid? PlantillaDtoIdRequest { get; private set; }
        public Guid? PlantillaDtoResponse { get; private set; }
        public string Version { get; private set; } = "1.0.0";
        public string? EndpointEspecifico { get; private set; }
        public string? Parametros { get; private set; }
        public string? MensajesError { get; private set; }
        public bool IsPublic { get; private set; } = false;

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
            Guid? plantillaDtoIdRequest,
            Guid? plantillaDtoIdResponse,
            string version,
            bool isPublic,
            string? endpointEspecifico = null,
            string? parametros = null,
            string? mensajesError = null)
            : base(id, name, description, type, code, isActive, createdDate, updatedDate)
        {
            if (proyectoId == Guid.Empty)
                throw new ArgumentException("El ProyectoId proporcionado no es válido.");

            if (configuracionDocumentacionId == Guid.Empty)
                throw new ArgumentException("El ConfiguracionDocumentacionId proporcionado no es válido.");

            ValidateVersion(version);

            ProyectoId = proyectoId;
            ConfiguracionDocumentacionId = configuracionDocumentacionId;
            PlantillaDtoIdRequest = plantillaDtoIdRequest;
            PlantillaDtoResponse = plantillaDtoIdResponse;
            Version = string.IsNullOrWhiteSpace(version) ? "1.0.0" : version.Trim();
            EndpointEspecifico = endpointEspecifico;
            Parametros = parametros;
            MensajesError = mensajesError;
            IsPublic = isPublic;
        }

        public void UpdateDocumentation(string newName, string newLastName, string newVersion, Guid? newPlantillaDtoIdResponse, Guid? newPlantillaDtoIdRequest)
        {
            UpdateMetadata(newName, newLastName, this.Code);
            UpdateVersion(newVersion);
            ChangePlantillas(newPlantillaDtoIdRequest, newPlantillaDtoIdResponse);
        }

        public void UpdateAdditionalFields(string? endpointEspecifico, string? parametros, string? mensajesError)
        {
            EndpointEspecifico = endpointEspecifico;
            Parametros = parametros;
            MensajesError = mensajesError;
        }

        public void UpdateDocumentation(
            string newName,
            string newDescription,
            string newVersion,
            Guid? newPlantillaDtoIdRequest,
            Guid? newPlantillaDtoIdResponse,
            string? endpointEspecifico = null,
            string? parametros = null,
            string? mensajesError = null)
        {
            UpdateMetadata(newName, newDescription, this.Code);
            UpdateVersion(newVersion);
            ChangePlantillas(newPlantillaDtoIdRequest, newPlantillaDtoIdResponse);
            UpdateAdditionalFields(endpointEspecifico, parametros, mensajesError);
        }

        public void UpdateVersion(string newVersion)
        {
            ValidateVersion(newVersion);
            Version = newVersion.Trim();
        }

        public void ChangePlantillas(Guid? newPlantillaDtoIdRequest, Guid? newPlantillaDtoIdResponse)
        {
            if (newPlantillaDtoIdRequest == Guid.Empty && newPlantillaDtoIdResponse == Guid.Empty)
                throw new ArgumentException("La nueva plantilla no es válida.");

            PlantillaDtoResponse = newPlantillaDtoIdResponse;
            PlantillaDtoIdRequest = newPlantillaDtoIdRequest;
        }

        private static void ValidateVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentException("El campo de versión es obligatorio.");
        }
    }
}