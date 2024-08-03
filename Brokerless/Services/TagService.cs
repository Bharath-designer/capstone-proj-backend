using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Repositories;

namespace Brokerless.Services
{

    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository) {
            _tagRepository = tagRepository;
        }
        public async Task<List<string>> GetTagsWithQueryString(string? query)
        {
            var tags = await _tagRepository.GetTagsWithQueryString(query);
            return tags;
        }
    }
}
