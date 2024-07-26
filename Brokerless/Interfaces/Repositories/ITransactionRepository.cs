using Brokerless.Models;

namespace Brokerless.Interfaces.Repositories
{
    public interface ITransactionRepository: IBaseRepository<Transaction, string>
    {
        public Task<Transaction> GetTransactionWithAllNavProperties(string transactionId);
    }
}
