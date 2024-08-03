using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;

namespace Brokerless.Repositories
{
    public class PropertyFileRepository : BaseRepository<PropertyFile, int>, IPropertyFileRepository
    {
        public PropertyFileRepository(BrokerlessDBContext _context) : base(_context)
        {
        }
    }
}
