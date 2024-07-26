using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;

namespace Brokerless.Repositories
{
    public class PropertyRepository: BaseRepository<Property,int>, IPropertyRepository
    {
        public PropertyRepository(BrokerlessDBContext _context) : base(_context) { }
    }
}
