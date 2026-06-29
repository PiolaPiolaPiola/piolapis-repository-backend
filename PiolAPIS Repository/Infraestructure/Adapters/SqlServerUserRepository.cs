using PiolAPIS_Repository.Application.Ports.Out;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;
using PiolAPIS_Repository.Infraestructure.Persistence.Models;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class SqlServerUserRepository : IUserRepository
    {
        private readonly PiolapisDbContext _context;

        public SqlServerUserRepository(PiolapisDbContext context) 
        {
            _context = context;
        }

        public void SaveAsync(User user)
        {
            var dbModel = new UsersDbModel()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };
        }
    }
}
