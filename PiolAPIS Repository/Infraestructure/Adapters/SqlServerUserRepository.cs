using PiolAPIS_Repository.Application.Ports;
using PiolAPIS_Repository.Domain.Entities;
using PiolAPIS_Repository.Infraestructure.Persistence;

namespace PiolAPIS_Repository.Infraestructure.Adapters
{
    public class SqlServerUserRepository : IUserRepository
    {
        private readonly PiolapisDbContext _context;

        public SqlServerUserRepository(PiolapisDbContext context) 
        {
            _context = context;
        }

        public void Save(User user)
        {

        }
    }
}
