using Walutoszpieg.Repositories;

namespace Walutoszpieg.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Walutoszpieg.Model;

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly TransactionHistoryRepository _transactionHistory;

        public TransactionHistoryController(TransactionHistoryRepository transactionHistory)
        {
            _transactionHistory = transactionHistory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionHistory>>> GetTransactionHistory()
        {
            var TransactionHistory = await _transactionHistory.GetTransactionHistoriesAsync();
            return Ok(TransactionHistory);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionHistory>> GetTransactionHistory(int id)
        {
            var transactionHistory = await _transactionHistory.GetTransactionHistoryByIdAsync(id);
            if (transactionHistory == null) return NotFound();
            return Ok(transactionHistory);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionHistory>> CreateTransactionHistory(TransactionHistory transactionHistory)
        {
            transactionHistory.Id = await _transactionHistory.CreateTransactionHistoryAsync(transactionHistory);
            return CreatedAtAction(nameof(GetTransactionHistory), new { id = transactionHistory.Id }, transactionHistory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionHistory(int id, TransactionHistory transactionHistory)
        {
            if (id != transactionHistory.Id) return BadRequest();
            var affectedRows = await _transactionHistory.UpdateTransactionHistoryAsync(transactionHistory);
            if (affectedRows == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionHistory(int id)
        {
            var affectedRows = await _transactionHistory.DeleteTransactionHistoryAsync(id);
            if (affectedRows == 0) return NotFound();
            return NoContent();
        }
    }
}
