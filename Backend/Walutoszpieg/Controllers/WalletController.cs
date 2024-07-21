using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Walutoszpieg.Model;
using Walutoszpieg.Repositories;

namespace Walutoszpieg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly WalletRepository _walletRepository;

        public WalletController(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallets(int userId)
        {
            var wallets = await _walletRepository.GetUserWallets(userId);
            return Ok(wallets);
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet([FromBody] Wallet wallet)
        {
            var id = await _walletRepository.AddWallet(wallet);
            return CreatedAtAction(nameof(GetUserWallets), new { userId = wallet.UserId }, id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWallet([FromBody] Wallet wallet)
        {
            await _walletRepository.UpdateWallet(wallet);
            return NoContent();
        }

        [HttpDelete("{userId}/{currencyCode}")]
        public async Task<IActionResult> DeleteWallet(int userId, string currencyCode)
        {
            await _walletRepository.DeleteWallet(userId, currencyCode);
            return NoContent();
        }

        [HttpPost("convert")]
        public async Task<IActionResult> ConvertCurrency([FromBody] ConvertCurrencyRequest request)
        {
            var rates = await FetchCurrencyRates();
            var fromRate = rates.SingleOrDefault(r => r.Code == request.ToCurrency)?.Mid ?? 0;
            var toRate = rates.SingleOrDefault(r => r.Code == request.FromCurrency)?.Mid ?? 0;

            if (fromRate == 0 || toRate == 0)
            {
                return BadRequest("Invalid currency code.");
            }

            var conversionRate = toRate / fromRate;
            await _walletRepository.ConvertCurrency(request.UserId, request.FromCurrency, request.ToCurrency, request.Amount, conversionRate);

            return NoContent();
        }

        private async Task<IEnumerable<Rate>> FetchCurrencyRates()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://api.nbp.pl/api/exchangerates/tables/A?format=json");
            var tables = JsonConvert.DeserializeObject<List<ExchangeRateTable>>(response);
            var rates = tables[0].Rates;
            rates.Add(new Rate { Currency = "polski złoty", Code = "PLN", Mid = 1 });
            return rates;
        }
        [HttpPost("addOrUpdate")]
        public async Task<IActionResult> AddOrUpdateWallet([FromBody] Wallet wallet)
        {
            await _walletRepository.AddOrUpdateWallet(wallet);
            return NoContent();
        }
    }
   

}
