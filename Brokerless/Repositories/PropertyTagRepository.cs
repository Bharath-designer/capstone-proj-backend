using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;

namespace Brokerless.Repositories
{
    public class PropertyTagRepository : BaseRepository<PropertyTag, int>, IPropertyTagRepository
    {
        public PropertyTagRepository(BrokerlessDBContext _context) : base(_context)
        {
        }
    }
}
