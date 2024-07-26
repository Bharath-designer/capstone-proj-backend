using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Repositories
{
    public class TransactionRepository: BaseRepository<Transaction, string>, ITransactionRepository
    {
        public TransactionRepository(BrokerlessDBContext _context) : base(_context) { }

        public async Task<Transaction> GetTransactionWithAllNavProperties(string transactionId)
        {
            var transaction = await _context.Transactions
                .Include(t=>t.User)
                .Include(t=>t.SubscriptionTemplate)
                .FirstOrDefaultAsync(t=>t.TransactionId == transactionId);

            return transaction;
        }
    }
}
