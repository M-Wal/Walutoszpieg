using Dapper;
using Walutoszpieg.Model;
using Walutoszpieg.DAL;

namespace Walutoszpieg.Repositories
{
    public class AlertRepository
    {
        private readonly DapperContext _context;

        public AlertRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alert>> GetAlertsAsync()
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryAsync<Alert>("SELECT * FROM alerts");
            }
        }

        public async Task<Alert> GetAlertByIdAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Alert>("SELECT * FROM alerts WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateAlertAsync(Alert alert)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            INSERT INTO alerts (user_id, currency_id, alert_type, threshold, created_at, notified_at)
            VALUES (@UserId, @CurrencyId, @AlertType, @Threshold, @CreatedAt, @NotifiedAt);
            SELECT CAST(SCOPE_IDENTITY() as int)";
                return await db.QuerySingleAsync<int>(query, alert);
            }
        }

        public async Task<int> UpdateAlertAsync(Alert alert)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            UPDATE alerts
            SET user_id = @UserId, currency_id = @CurrencyId, alert_type = @AlertType, threshold = @Threshold, notified_at = @NotifiedAt
            WHERE id = @Id";
                return await db.ExecuteAsync(query, alert);
            }
        }

        public async Task<int> DeleteAlertAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.ExecuteAsync("DELETE FROM alerts WHERE id = @Id", new { Id = id });
            }
        }
    }
}
