using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;

namespace Brokerless.Repositories
{
    public class SubscriptionTemplateRepository : BaseRepository<SubscriptionTemplate, string>, ISubscriptionTemplateRepository
    {
        public SubscriptionTemplateRepository(BrokerlessDBContext _context) : base(_context)
        {
        }
    }
}
