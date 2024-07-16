using Microsoft.AspNetCore.Mvc;
using Walutoszpieg.Model;
using Walutoszpieg.Repositories;

namespace Walutoszpieg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly WalletRepository _walletService;

        public WalletController(WalletRepository walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet(Wallet wallet)
        {
            var walletId = await _walletService.AddWallet(wallet);
            return CreatedAtAction(nameof(GetUserWallets), new { userId = wallet.UserId }, wallet);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWallet(Wallet wallet)
        {
            await _walletService.UpdateWallet(wallet);
            return NoContent();
        }

        [HttpDelete("{userId}/{currencyCode}")]
        public async Task<IActionResult> DeleteWallet(int userId, string currencyCode)
        {
            await _walletService.DeleteWallet(userId, currencyCode);
            return NoContent();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallets(int userId)
        {
            var wallets = await _walletService.GetUserWallets(userId);
            return Ok(wallets);
        }
    }
}
