using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.DocumentationSetting;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class DocumentationSettingRepository : IDocumentationSettingRepository
    {
        private readonly PiolapisDbContext _context;

        public DocumentationSettingRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(DocumentationSettings documentationSetting)
        {
            var dbModel = new DocumentationSettingsDbModel
            {
                Id = documentationSetting.Id,
                Name = documentationSetting.Name,
                Description = documentationSetting.Description,
                Type = documentationSetting.Type,
                Code = documentationSetting.Code,
                IsActive = documentationSetting.IsActive,
                CreatedDate = documentationSetting.CreatedDate,
                UpdatedDate = documentationSetting.UpdatedDate,
                BaseEndpoint = documentationSetting.BaseEndpoint,
                ApiType = documentationSetting.ApiType,
                ProyectoId = documentationSetting.ProyectoId
            };

            _context.DocumentationSettings.Add(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocumentationSettings documentationSetting)
        {
            var dbModel = await _context.DocumentationSettings.FindAsync(documentationSetting.Id);
            if (dbModel == null)
                throw new KeyNotFoundException($"No se encontró la configuración de documentación para actualizar con ID: {documentationSetting.Id}");

            dbModel.Name = documentationSetting.Name;
            dbModel.Description = documentationSetting.Description;
            dbModel.Type = documentationSetting.Type;
            dbModel.Code = documentationSetting.Code;
            dbModel.IsActive = documentationSetting.IsActive;
            dbModel.UpdatedDate = documentationSetting.UpdatedDate;
            dbModel.BaseEndpoint = documentationSetting.BaseEndpoint;
            dbModel.ApiType = documentationSetting.ApiType;
            dbModel.ProyectoId = documentationSetting.ProyectoId;

            _context.DocumentationSettings.Update(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var dbModel = await _context.DocumentationSettings.FindAsync(id);
            if (dbModel == null) return false;

            var domainSetting = MapToDomain(dbModel);

            if (isActive)
                domainSetting.Activate();
            else
                domainSetting.Deactivate();

            dbModel.IsActive = domainSetting.IsActive;
            dbModel.UpdatedDate = domainSetting.UpdatedDate;

            _context.DocumentationSettings.Update(dbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DocumentationSettings?> GetByIdAsync(Guid id)
        {
            var dbModel = await _context.DocumentationSettings.FindAsync(id);
            if (dbModel == null) return null;

            return MapToDomain(dbModel);
        }

        public async Task<IEnumerable<DocumentationSettings>> GetAllAsync()
        {
            var dbModels = await _context.DocumentationSettings.ToListAsync();
            return dbModels.Select(MapToDomain);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.DocumentationSettings.AnyAsync(ds => ds.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var dbModel = await _context.DocumentationSettings.FindAsync(id);
            if (dbModel != null)
            {
                _context.DocumentationSettings.Remove(dbModel);
                await _context.SaveChangesAsync();
            }
        }

        private static DocumentationSettings MapToDomain(DocumentationSettingsDbModel dbModel)
        {
            return new DocumentationSettings(
                id: dbModel.Id,
                name: dbModel.Name,
                description: dbModel.Description,
                type: dbModel.Type,
                code: dbModel.Code,
                isActive: dbModel.IsActive,
                createdDate: dbModel.CreatedDate,
                updatedDate: dbModel.UpdatedDate,
                baseEndpoint: dbModel.BaseEndpoint,
                apiType: dbModel.ApiType,
                proyectoId: dbModel.ProyectoId
            );
        }
    }
}