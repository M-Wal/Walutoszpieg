using Dapper;
using Walutoszpieg.Model;
using Walutoszpieg.DAL;

namespace Walutoszpieg.Repositories
{
    public class CurrencyRepository
    {
        private readonly DapperContext _context;

        public CurrencyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryAsync<Currency>("SELECT * FROM currencies");
            }
        }

        public async Task<Currency> GetCurrencyByIdAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Currency>("SELECT * FROM currencies WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateCurrencyAsync(Currency currency)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            INSERT INTO currencies (currency_code, currency_name)
            VALUES (@CurrencyCode, @CurrencyName);
            SELECT CAST(SCOPE_IDENTITY() as int)";
                return await db.QuerySingleAsync<int>(query, currency);
            }
        }

        public async Task<int> UpdateCurrencyAsync(Currency currency)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            UPDATE currencies
            SET currency_code = @CurrencyCode, currency_name = @CurrencyName
            WHERE id = @Id";
                return await db.ExecuteAsync(query, currency);
            }
        }

        public async Task<int> DeleteCurrencyAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.ExecuteAsync("DELETE FROM currencies WHERE id = @Id", new { Id = id });
            }
        }

    }
}
