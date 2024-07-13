using Dapper;
using Walutoszpieg.Model;
using Walutoszpieg.DAL;

namespace Walutoszpieg.Repositories
{
    public class ExchangeRateRepository
    {
        private readonly DapperContext _context;

        public ExchangeRateRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExchangeRate>> GetExchangeRatesAsync()
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryAsync<ExchangeRate>("SELECT * FROM exchange_rates");
            }
        }

        public async Task<ExchangeRate> GetExchangeRateByIdAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<ExchangeRate>("SELECT * FROM exchange_rates WHERE id = @Id",
                    new { Id = id });
            }
        }

        public async Task<int> CreateExchangeRateAsync(ExchangeRate exchangeRate)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            INSERT INTO exchange_rates (currency_id, rate, timestamp)
            VALUES (@CurrencyId, @Rate, @Timestamp);
            SELECT CAST(SCOPE_IDENTITY() as int)";
                return await db.QuerySingleAsync<int>(query, exchangeRate);
            }
        }

        public async Task<int> UpdateExchangeRateAsync(ExchangeRate exchangeRate)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            UPDATE exchange_rates
            SET currency_id = @CurrencyId, rate = @Rate, timestamp = @Timestamp
            WHERE id = @Id";
                return await db.ExecuteAsync(query, exchangeRate);
            }
        }

        public async Task<int> DeleteExchangeRateAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.ExecuteAsync("DELETE FROM exchange_rates WHERE id = @Id", new { Id = id });
            }
        }
    }
}
