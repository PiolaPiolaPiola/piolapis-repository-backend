using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.CodeMessage;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class CodeMessageRepository : ICodeMessageRepository
    {
        private readonly PiolapisDbContext _context;

        public CodeMessageRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(CodeMessages codeMessage)
        {
            var dbModel = new CodeMessagesDbModel
            {
                Id = codeMessage.Id,
                Name = codeMessage.Name,
                Description = codeMessage.Description,
                Type = codeMessage.Type,
                Code = codeMessage.Code,
                IsActive = codeMessage.IsActive,
                CreatedDate = codeMessage.CreatedDate,
                UpdatedDate = codeMessage.UpdatedDate,
                HttpCode = codeMessage.HttpCode,
                Response = codeMessage.Response,
                ResponseType = codeMessage.ResponseType
            };

            _context.CodeMessages.Add(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CodeMessages codeMessage)
        {
            var dbModel = await _context.CodeMessages.FindAsync(codeMessage.Id);
            if (dbModel == null)
                throw new KeyNotFoundException($"No se encontró el mensaje código para actualizar con ID: {codeMessage.Id}");

            dbModel.Name = codeMessage.Name;
            dbModel.Description = codeMessage.Description;
            dbModel.Type = codeMessage.Type;
            dbModel.Code = codeMessage.Code;
            dbModel.IsActive = codeMessage.IsActive;
            dbModel.UpdatedDate = codeMessage.UpdatedDate;
            dbModel.HttpCode = codeMessage.HttpCode;
            dbModel.Response = codeMessage.Response;
            dbModel.ResponseType = codeMessage.ResponseType;

            _context.CodeMessages.Update(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var dbModel = await _context.CodeMessages.FindAsync(id);
            if (dbModel == null) return false;

            var domainModel = MapToDomain(dbModel);

            if (isActive)
                domainModel.Activate();
            else
                domainModel.Deactivate();

            dbModel.IsActive = domainModel.IsActive;
            dbModel.UpdatedDate = domainModel.UpdatedDate;

            _context.CodeMessages.Update(dbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CodeMessages?> GetByIdAsync(Guid id)
        {
            var dbModel = await _context.CodeMessages.FindAsync(id);
            if (dbModel == null) return null;

            return MapToDomain(dbModel);
        }

        public async Task<IEnumerable<CodeMessages>> GetAllAsync()
        {
            var dbModels = await _context.CodeMessages.ToListAsync();
            return dbModels.Select(MapToDomain);
        }

        public async Task<IEnumerable<CodeMessages>> GetAllByHttpCodeAsync(string httpCode)
        {
            var dbModels = await _context.CodeMessages
                .Where(cm => cm.HttpCode.Equals(httpCode, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return dbModels.Select(MapToDomain);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.CodeMessages.AnyAsync(cm => cm.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var dbModel = await _context.CodeMessages.FindAsync(id);
            if (dbModel != null)
            {
                _context.CodeMessages.Remove(dbModel);
                await _context.SaveChangesAsync();
            }
        }

        private static CodeMessages MapToDomain(CodeMessagesDbModel dbModel)
        {
            return new CodeMessages(
                id: dbModel.Id,
                name: dbModel.Name,
                description: dbModel.Description,
                type: dbModel.Type,
                code: dbModel.Code,
                isActive: dbModel.IsActive,
                createdDate: dbModel.CreatedDate,
                updatedDate: dbModel.UpdatedDate,
                httpCode: dbModel.HttpCode,
                response: dbModel.Response,
                responseType: dbModel.ResponseType
            );
        }
    }
}