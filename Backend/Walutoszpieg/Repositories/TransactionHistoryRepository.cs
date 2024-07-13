using Dapper;
using Walutoszpieg.Model;
using Walutoszpieg.DAL;

namespace Walutoszpieg.Repositories
{
    public class TransactionHistoryRepository
    {
        private readonly DapperContext _context;

        public TransactionHistoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionHistory>> GetTransactionHistoriesAsync()
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryAsync<TransactionHistory>("SELECT * FROM transaction_history");
            }
        }

        public async Task<TransactionHistory> GetTransactionHistoryByIdAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<TransactionHistory>("SELECT * FROM transaction_history WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateTransactionHistoryAsync(TransactionHistory transactionHistory)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            INSERT INTO transaction_history (user_id, currency_from_id, currency_to_id, amount, rate, timestamp)
            VALUES (@UserId, @CurrencyFromId, @CurrencyToId, @Amount, @Rate, @Timestamp);
            SELECT CAST(SCOPE_IDENTITY() as int)";
                return await db.QuerySingleAsync<int>(query, transactionHistory);
            }
        }

        public async Task<int> UpdateTransactionHistoryAsync(TransactionHistory transactionHistory)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            UPDATE transaction_history
            SET user_id = @UserId, currency_from_id = @CurrencyFromId, currency_to_id = @CurrencyToId, amount = @Amount, rate = @Rate, timestamp = @Timestamp
            WHERE id = @Id";
                return await db.ExecuteAsync(query, transactionHistory);
            }
        }

        public async Task<int> DeleteTransactionHistoryAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.ExecuteAsync("DELETE FROM transaction_history WHERE id = @Id", new { Id = id });
            }
        }
    }
}
