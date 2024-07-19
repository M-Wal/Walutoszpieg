using Dapper;
using Newtonsoft.Json;
using Walutoszpieg.DAL;
using Walutoszpieg.Model;

public class WalletRepository
{
    private readonly DapperContext _context;

    public WalletRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> AddWallet(Wallet wallet)
    {
        var query = "INSERT INTO Wallets (UserId, CurrencyCode, Amount) VALUES (@UserId, @CurrencyCode, @Amount);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<int>(query, wallet);
        }
    }

    public async Task<int> UpdateWallet(Wallet wallet)
    {
        var query = "UPDATE Wallets SET Amount = @Amount WHERE UserId = @UserId AND CurrencyCode = @CurrencyCode";
        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, wallet);
        }
    }

    public async Task<int> DeleteWallet(int userId, string currencyCode)
    {
        var query = "DELETE FROM Wallets WHERE UserId = @UserId AND CurrencyCode = @CurrencyCode";
        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, new { UserId = userId, CurrencyCode = currencyCode });
        }
    }

    public async Task<IEnumerable<Wallet>> GetUserWallets(int userId)
    {
        var query = "SELECT * FROM Wallets WHERE UserId = @UserId";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryAsync<Wallet>(query, new { UserId = userId });
        }
    }

    // New method to convert currencies within the wallet
    public async Task ConvertCurrency(int userId, string fromCurrency, string toCurrency, decimal amount, decimal conversionRate)
    {
        using (var connection = _context.CreateConnection())
        {
            var fromWallet = await GetUserWallet(userId, fromCurrency);
            if (fromWallet == null || fromWallet.Amount < amount)
            {
                throw new InvalidOperationException("Insufficient funds or currency not found in wallet.");
            }

            fromWallet.Amount -= amount;
            await UpdateWallet(fromWallet);

            var toWallet = await GetUserWallet(userId, toCurrency);
            if (toWallet == null)
            {
                toWallet = new Wallet { UserId = userId, CurrencyCode = toCurrency, Amount = 0 };
                await AddWallet(toWallet);
            }
            toWallet.Amount += amount * conversionRate;

            await UpdateWallet(toWallet);
        }
    }

    private async Task<IEnumerable<Rate>> FetchCurrencyRates()
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetStringAsync("https://api.nbp.pl/api/exchangerates/tables/A?format=json");
            var data = JsonConvert.DeserializeObject<List<ExchangeRateTable>>(response);
            var rates = data[0].Rates.ToList();
            rates.Add(new Rate { Code = "PLN", Currency = "polski złoty", Mid = 1 });
            return rates;
        }
    }
    public async Task AddOrUpdateWallet(Wallet wallet)
    {
        var existingWallet = await GetUserWallet(wallet.UserId, wallet.CurrencyCode);
        if (existingWallet != null)
        {
            existingWallet.Amount += wallet.Amount;
            await UpdateWallet(existingWallet);
        }
        else
        {
            await AddWallet(wallet);
        }
    }

    private async Task<Wallet> GetUserWallet(int userId, string currencyCode)
    {
        var query = "SELECT * FROM Wallets WHERE UserId = @UserId AND CurrencyCode = @CurrencyCode";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Wallet>(query, new { UserId = userId, CurrencyCode = currencyCode });
        }
    }
}
