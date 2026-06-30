using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.Documentation;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class DocumentationRepository : IDocumentationRepository
    {
        private readonly PiolapisDbContext _context;

        public DocumentationRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Documentations documentation)
        {
            var dbModel = new DocumentationsDbModel
            {
                Id = documentation.Id,
                Name = documentation.Name,
                Description = documentation.Description,
                Type = documentation.Type,
                Code = documentation.Code,
                IsActive = documentation.IsActive,
                CreatedDate = documentation.CreatedDate,
                UpdatedDate = documentation.UpdatedDate,
                ProyectoId = documentation.ProyectoId,
                ConfiguracionDocumentacionId = documentation.ConfiguracionDocumentacionId,
                PlantillaDtoIdRequest = documentation.PlantillaDtoIdRequest,
                PlantillaDtoResponse = documentation.PlantillaDtoResponse,
                Version = documentation.Version
            };

            _context.Documentations.Add(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Documentations documentation)
        {
            var dbModel = await _context.Documentations.FindAsync(documentation.Id);
            if (dbModel == null)
                throw new KeyNotFoundException($"No se encontró el registro de documentación para actualizar con ID: {documentation.Id}");

            dbModel.Name = documentation.Name;
            dbModel.Description = documentation.Description;
            dbModel.Type = documentation.Type;
            dbModel.Code = documentation.Code;
            dbModel.IsActive = documentation.IsActive;
            dbModel.UpdatedDate = documentation.UpdatedDate;
            dbModel.ProyectoId = documentation.ProyectoId;
            dbModel.ConfiguracionDocumentacionId = documentation.ConfiguracionDocumentacionId;
            dbModel.PlantillaDtoResponse = documentation.PlantillaDtoResponse;
            dbModel.PlantillaDtoIdRequest = documentation.PlantillaDtoIdRequest;
            dbModel.Version = documentation.Version;
            dbModel.EndpointEspecifico = documentation.EndpointEspecifico;
            dbModel.Parametros = documentation.Parametros;
            dbModel.MensajesError = documentation.MensajesError;
            dbModel.IsPublic = documentation.IsPublic;

            _context.Documentations.Update(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var dbModel = await _context.Documentations.FindAsync(id);
            if (dbModel == null) return false;

            var domainDoc = MapToDomain(dbModel);

            if (isActive)
                domainDoc.Activate();
            else
                domainDoc.Deactivate();

            dbModel.IsActive = domainDoc.IsActive;
            dbModel.UpdatedDate = domainDoc.UpdatedDate;

            _context.Documentations.Update(dbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Documentations?> GetByIdAsync(Guid id)
        {
            var dbModel = await _context.Documentations.FindAsync(id);
            if (dbModel == null) return null;

            return MapToDomain(dbModel);
        }

        public async Task<IEnumerable<Documentations>> GetAllAsync(Guid? proyectoId = null)
        {
            IQueryable<DocumentationsDbModel> query = _context.Documentations;

            if (proyectoId.HasValue)
            {
                query = query.Where(d => d.ProyectoId == proyectoId.Value);
            }

            var dbModels = await query.ToListAsync();
            return dbModels.Select(MapToDomain);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.Documentations.AnyAsync(d => d.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var dbModel = await _context.Documentations.FindAsync(id);
            if (dbModel != null)
            {
                _context.Documentations.Remove(dbModel);
                await _context.SaveChangesAsync();
            }
        }

        private static Documentations MapToDomain(DocumentationsDbModel dbModel)
        {
            return new Documentations(
                id: dbModel.Id,
                name: dbModel.Name,
                description: dbModel.Description,
                type: dbModel.Type,
                code: dbModel.Code,
                isActive: dbModel.IsActive,
                createdDate: dbModel.CreatedDate,
                updatedDate: dbModel.UpdatedDate,
                proyectoId: dbModel.ProyectoId,
                configuracionDocumentacionId: dbModel.ConfiguracionDocumentacionId,
                plantillaDtoIdRequest: dbModel.PlantillaDtoIdRequest,
                plantillaDtoIdResponse: dbModel.PlantillaDtoResponse,
                version: dbModel.Version,
                endpointEspecifico: dbModel.EndpointEspecifico,
                parametros: dbModel.Parametros,
                mensajesError: dbModel.MensajesError,
                isPublic: dbModel.IsPublic
            );
        }
    }
}