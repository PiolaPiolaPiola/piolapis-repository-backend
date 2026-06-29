using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using PiolAPIS_Repository.Application.Ports.Variable;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class VariableRepository : IVariableRepository
    {
        private readonly PiolapisDbContext _context;

        public VariableRepository(PiolapisDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Variables variable)
        {
            var dbModel = new VariablesDbModel
            {
                Id = variable.Id,
                Name = variable.Name,
                Description = variable.Description,
                Type = variable.Type,
                Code = variable.Code,
                IsActive = variable.IsActive,
                CreatedDate = variable.CreatedDate,
                UpdatedDate = variable.UpdatedDate,
                DataType = variable.DataType,
                ExampleValue = variable.ExampleValue
            };

            _context.Variables.Add(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Variables variable)
        {
            var dbModel = await _context.Variables.FindAsync(variable.Id);
            if (dbModel == null)
                throw new KeyNotFoundException($"No se encontró la variable para actualizar con ID: {variable.Id}");

            dbModel.Name = variable.Name;
            dbModel.Description = variable.Description;
            dbModel.Type = variable.Type;
            dbModel.Code = variable.Code;
            dbModel.IsActive = variable.IsActive;
            dbModel.UpdatedDate = variable.UpdatedDate;
            dbModel.DataType = variable.DataType;
            dbModel.ExampleValue = variable.ExampleValue;

            _context.Variables.Update(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid id, bool isActive)
        {
            var dbModel = await _context.Variables.FindAsync(id);
            if (dbModel == null) return false;

            var domainVariable = MapToDomain(dbModel);

            if (isActive)
                domainVariable.Activate();
            else
                domainVariable.Deactivate();

            dbModel.IsActive = domainVariable.IsActive;
            dbModel.UpdatedDate = domainVariable.UpdatedDate;

            _context.Variables.Update(dbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Variables?> GetByIdAsync(Guid id)
        {
            var dbModel = await _context.Variables.FindAsync(id);
            if (dbModel == null) return null;

            return MapToDomain(dbModel);
        }

        public async Task<IEnumerable<Variables>> GetAllAsync()
        {
            var dbModels = await _context.Variables.ToListAsync();
            return dbModels.Select(MapToDomain);
        }

        public async Task<bool> ValidateExistsAsync(Guid id)
        {
            return await _context.Variables.AnyAsync(v => v.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var dbModel = await _context.Variables.FindAsync(id);
            if (dbModel != null)
            {
                _context.Variables.Remove(dbModel);
                await _context.SaveChangesAsync();
            }
        }

        private static Variables MapToDomain(VariablesDbModel dbModel)
        {
            return new Variables(
                id: dbModel.Id,
                name: dbModel.Name,
                description: dbModel.Description,
                type: dbModel.Type,
                code: dbModel.Code,
                isActive: dbModel.IsActive,
                createdDate: dbModel.CreatedDate,
                updatedDate: dbModel.UpdatedDate,
                dataType: dbModel.DataType,
                exampleValue: dbModel.ExampleValue
            );
        }
    }
}