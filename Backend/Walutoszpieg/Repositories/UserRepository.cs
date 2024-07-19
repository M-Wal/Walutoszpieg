using Dapper;
using Walutoszpieg.Model;
using Walutoszpieg.DAL;

namespace Walutoszpieg.Repositories
{
    public class UserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryAsync<User>("SELECT * FROM users");
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
                INSERT INTO users (username, email, password_hash, created_at, updated_at)
                VALUES (@Username, @Email, @PasswordHash, @CreatedAt, @UpdatedAt);
                SELECT CAST(SCOPE_IDENTITY() as int)";
                return await db.QuerySingleAsync<int>(query, user);
            }
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            using (var db = _context.CreateConnection())
            {
                var query = @"
                UPDATE users
                SET username = @Username, email = @Email, password_hash = @PasswordHash, updated_at = @UpdatedAt
                WHERE id = @Id";
                return await db.ExecuteAsync(query, user);
            }
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            using (var db = _context.CreateConnection())
            {
                return await db.ExecuteAsync("DELETE FROM users WHERE id = @Id", new { Id = id });
            }
        }

    }
}
