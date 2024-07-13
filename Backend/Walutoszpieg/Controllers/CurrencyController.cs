using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walutoszpieg.Model;
using Walutoszpieg.Repositories;

namespace Walutoszpieg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyRepository _currency;

        public CurrencyController(CurrencyRepository currency)
        {
            _currency = currency;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            return Ok(await _currency.GetCurrenciesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
            var currency = await _currency.GetCurrencyByIdAsync(id);
            if (currency == null) return NotFound();
            return Ok(currency);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCurrency(Currency currency)
        {
            var id = await _currency.CreateCurrencyAsync(currency);
            return CreatedAtAction(nameof(GetCurrency), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrency(int id, Currency currency)
        {
            if (id != currency.Id) return BadRequest();
            var result = await _currency.UpdateCurrencyAsync(currency);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var result = await _currency.DeleteCurrencyAsync(id);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}
