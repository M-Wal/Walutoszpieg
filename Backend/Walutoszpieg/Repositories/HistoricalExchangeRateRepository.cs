using Dapper;
using Walutoszpieg.Model;
using Walutoszpieg.DAL;

namespace Walutoszpieg.Repositories
{
    public class HistoricalExchangeRateRepository
    {
        private readonly DapperContext _context;

        public HistoricalExchangeRateRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoricalExchangeRate>> GetHistoricalExchangeRatesAsync()
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryAsync<HistoricalExchangeRate>("SELECT * FROM historical_exchange_rates");
            }
        }

        public async Task<HistoricalExchangeRate> GetHistoricalExchangeRateByIdAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<HistoricalExchangeRate>("SELECT * FROM historical_exchange_rates WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateHistoricalExchangeRateAsync(HistoricalExchangeRate historicalExchangeRate)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            INSERT INTO historical_exchange_rates (currency_id, rate, timestamp)
            VALUES (@CurrencyId, @Rate, @Timestamp);
            SELECT CAST(SCOPE_IDENTITY() as int)";
                return await db.QuerySingleAsync<int>(query, historicalExchangeRate);
            }
        }

        public async Task<int> UpdateHistoricalExchangeRateAsync(HistoricalExchangeRate historicalExchangeRate)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            UPDATE historical_exchange_rates
            SET currency_id = @CurrencyId, rate = @Rate, timestamp = @Timestamp
            WHERE id = @Id";
                return await db.ExecuteAsync(query, historicalExchangeRate);
            }
        }

        public async Task<int> DeleteHistoricalExchangeRateAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.ExecuteAsync("DELETE FROM historical_exchange_rates WHERE id = @Id", new { Id = id });
            }
        }
    }
}
