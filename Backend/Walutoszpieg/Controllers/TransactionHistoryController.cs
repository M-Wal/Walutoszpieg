using Walutoszpieg.Repositories;
using Microsoft.AspNetCore.Mvc;
using Walutoszpieg.Model;

namespace Walutoszpieg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly TransactionHistoryRepository _repository;

        public TransactionHistoryController(TransactionHistoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionHistories()
        {
            var histories = await _repository.GetTransactionHistoriesAsync();
            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionHistoryById(int id)
        {
            var history = await _repository.GetTransactionHistoryByIdAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionHistory(TransactionHistory transactionHistory)
        {
            var id = await _repository.CreateTransactionHistoryAsync(transactionHistory);
            transactionHistory.Id = id;
            return CreatedAtAction(nameof(GetTransactionHistoryById), new { id = id }, transactionHistory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionHistory(int id, TransactionHistory transactionHistory)
        {
            if (id != transactionHistory.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateTransactionHistoryAsync(transactionHistory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionHistory(int id)
        {
            await _repository.DeleteTransactionHistoryAsync(id);
            return NoContent();
        }
    }

}
