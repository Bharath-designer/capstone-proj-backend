namespace Brokerless.Interfaces.Repositories
{
    public interface IBaseRepository<T, K> where T : class
    {
        public Task<T> GetById(K id);
        public Task<List<T>> GetAll();
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(T entity);

    }
}
