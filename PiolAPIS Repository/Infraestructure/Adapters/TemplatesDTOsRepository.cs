using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.TemplateDTO;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class TemplatesDTOsRepository : ITemplatesDTOsRepository
    {
        private readonly PiolapisDbContext _context;

        public TemplatesDTOsRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(TemplatesDTOs templateDTO)
        {
            var dbModel = new TemplatesDTOsDbModel
            {
                Id = templateDTO.Id,
                Name = templateDTO.Name,
                Description = templateDTO.Description,
                Type = templateDTO.Type,
                Code = templateDTO.Code,
                IsActive = templateDTO.IsActive,
                CreatedDate = templateDTO.CreatedDate,
                UpdatedDate = templateDTO.UpdatedDate,
                RequestType = templateDTO.RequestType,
                Request = templateDTO.Request,
                Response = templateDTO.Response,
                ResponseType = templateDTO.ResponseType,
                IsShared = templateDTO.IsShared,
                Tags = templateDTO.Tags
            };

            _context.TemplatesDTOs.Add(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TemplatesDTOs templateDTO)
        {
            var dbModel = await _context.TemplatesDTOs.FindAsync(templateDTO.Id);
            if (dbModel == null)
                throw new KeyNotFoundException($"No se encontró la plantilla para actualizar con ID: {templateDTO.Id}");

            dbModel.Name = templateDTO.Name;
            dbModel.Description = templateDTO.Description;
            dbModel.Type = templateDTO.Type;
            dbModel.Code = templateDTO.Code;
            dbModel.IsActive = templateDTO.IsActive;
            dbModel.UpdatedDate = templateDTO.UpdatedDate;
            dbModel.RequestType = templateDTO.RequestType;
            dbModel.Request = templateDTO.Request;
            dbModel.Response = templateDTO.Response;
            dbModel.ResponseType = templateDTO.ResponseType;
            dbModel.IsShared = templateDTO.IsShared;
            dbModel.Tags = templateDTO.Tags;

            _context.TemplatesDTOs.Update(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var dbModel = await _context.TemplatesDTOs.FindAsync(id);
            if (dbModel == null) return false;

            var domainTemplate = MapToDomain(dbModel);
            if (isActive)
                domainTemplate.Activate();
            else
                domainTemplate.Deactivate();

            dbModel.IsActive = domainTemplate.IsActive;
            dbModel.UpdatedDate = domainTemplate.UpdatedDate;

            _context.TemplatesDTOs.Update(dbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TemplatesDTOs?> GetByIdAsync(Guid id)
        {
            var dbModel = await _context.TemplatesDTOs.FindAsync(id);
            if (dbModel == null) return null;

            return MapToDomain(dbModel);
        }

        public async Task<IEnumerable<TemplatesDTOs>> GetAllAsync()
        {
            var dbModels = await _context.TemplatesDTOs.ToListAsync();
            return dbModels.Select(MapToDomain);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.TemplatesDTOs.AnyAsync(t => t.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var dbModel = await _context.TemplatesDTOs.FindAsync(id);
            if (dbModel != null)
            {
                _context.TemplatesDTOs.Remove(dbModel);
                await _context.SaveChangesAsync();
            }
        }

        private static TemplatesDTOs MapToDomain(TemplatesDTOsDbModel dbModel)
        {
            return new TemplatesDTOs(
                id: dbModel.Id,
                name: dbModel.Name,
                description: dbModel.Description,
                type: dbModel.Type,
                code: dbModel.Code,
                isActive: dbModel.IsActive,
                createdDate: dbModel.CreatedDate,
                updatedDate: dbModel.UpdatedDate,
                requestType: dbModel.RequestType,
                request: dbModel.Request,
                response: dbModel.Response,
                responseType: dbModel.ResponseType,
                isShared: dbModel.IsShared,
                tags: dbModel.Tags
            );
        }
    }
}