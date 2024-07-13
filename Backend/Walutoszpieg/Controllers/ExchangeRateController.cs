using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walutoszpieg.Model;
using Walutoszpieg.Repositories;

namespace Walutoszpieg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly ExchangeRateRepository _exchangeRate;

        public ExchangeRateController(ExchangeRateRepository exchangeRate)
        {
            _exchangeRate = exchangeRate;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeRate>>> GetExchangeRates()
        {
            return Ok(await _exchangeRate.GetExchangeRatesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeRate>> GetExchangeRate(int id)
        {
            var exchangeRate = await _exchangeRate.GetExchangeRateByIdAsync(id);
            if (exchangeRate == null) return NotFound();
            return Ok(exchangeRate);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateExchangeRate(ExchangeRate exchangeRate)
        {
            var id = await _exchangeRate.CreateExchangeRateAsync(exchangeRate);
            return CreatedAtAction(nameof(GetExchangeRate), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExchangeRate(int id, ExchangeRate exchangeRate)
        {
            if (id != exchangeRate.Id) return BadRequest();
            var result = await _exchangeRate.UpdateExchangeRateAsync(exchangeRate);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExchangeRate(int id)
        {
            var result = await _exchangeRate.DeleteExchangeRateAsync(id);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}
