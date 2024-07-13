using Dapper;
using Walutoszpieg.Model;
using Walutoszpieg.DAL;

namespace Walutoszpieg.Repositories
{
    public class NotificationRepository
    {
        private readonly DapperContext _context;

        public NotificationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync()
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryAsync<Notification>("SELECT * FROM notifications");
            }
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Notification>("SELECT * FROM notifications WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateNotificationAsync(Notification notification)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            INSERT INTO notifications (user_id, message, read, created_at)
            VALUES (@UserId, @Message, @Read, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() as int)";
                return await db.QuerySingleAsync<int>(query, notification);
            }
        }

        public async Task<int> UpdateNotificationAsync(Notification notification)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
            UPDATE notifications
            SET user_id = @UserId, message = @Message, read = @Read, created_at = @CreatedAt
            WHERE id = @Id";
                return await db.ExecuteAsync(query, notification);
            }
        }

        public async Task<int> DeleteNotificationAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.ExecuteAsync("DELETE FROM notifications WHERE id = @Id", new { Id = id });
            }
        }
    }
}
