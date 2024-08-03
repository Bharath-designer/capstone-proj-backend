namespace Brokerless.Interfaces.Services
{
    public interface ITagService
    {
        public Task<List<string>> GetTagsWithQueryString(string? query);
    }
}
