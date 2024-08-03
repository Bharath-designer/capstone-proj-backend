using Brokerless.Models;

namespace Brokerless.Interfaces.Repositories
{
    public interface ITagRepository: IBaseRepository<Tag, string>
    {
        public Task<List<string>> GetTagsWithQueryString(string query);
    }
}
