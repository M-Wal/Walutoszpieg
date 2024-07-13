using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walutoszpieg.Model;
using Walutoszpieg.Repositories;

[Route("api/[controller]")]
[ApiController]
public class AlertController : ControllerBase
{
    private readonly AlertRepository _alert;

    public AlertController(AlertRepository alert)
    {
        _alert = alert;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Alert>>> GetAlerts()
    {
        return Ok(await _alert.GetAlertsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Alert>> GetAlert(int id)
    {
        var alert = await _alert.GetAlertByIdAsync(id);
        if (alert == null) return NotFound();
        return Ok(alert);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAlert(Alert alert)
    {
        var id = await _alert.CreateAlertAsync(alert);
        return CreatedAtAction(nameof(GetAlert), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAlert(int id, Alert alert)
    {
        if (id != alert.Id) return BadRequest();
        var result = await _alert.UpdateAlertAsync(alert);
        if (result == 0) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlert(int id)
    {
        var result = await _alert.DeleteAlertAsync(id);
        if (result == 0) return NotFound();
        return NoContent();
    }
}