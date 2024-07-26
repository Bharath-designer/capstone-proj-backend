using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;

namespace Brokerless.Repositories
{
    public class TagRepository : BaseRepository<Tag, string>, ITagRepository
    {
        public TagRepository(BrokerlessDBContext _context) : base(_context)
        {
        }
    }
}
