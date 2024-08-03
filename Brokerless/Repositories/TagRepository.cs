using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Repositories
{
    public class TagRepository : BaseRepository<Tag, string>, ITagRepository
    {
        public TagRepository(BrokerlessDBContext _context) : base(_context)
        {
        }

        public async Task<List<string>> GetTagsWithQueryString(string? query)
        {
            var tagQuery = _context.Tags.AsQueryable();

            if (query != null)
            {
                tagQuery = tagQuery.Where(t=>t.TagValue.Contains(query));
            }

            tagQuery = tagQuery.Take(25);
            var tags = await tagQuery.Select(t=> t.TagValue).ToListAsync();

            return tags;
        }
    }
}
