using ExpensesBE.DTO;
using ExpensesBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _context;

        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetExpenses")]
        public async Task<ActionResult<List<Expense>>> Get()
        {
            return await _context.expenses.ToListAsync();
        }

        [HttpPost(Name = "AddExpense")]
        public async Task<IActionResult> PostAsync([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("Expense cannot be null");
            }

            try
            {
                _context.expenses.Add(expense);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error adding expense: {ex.Message}");
            }
            return CreatedAtAction(nameof(Get), new { id = expense.id }, expense);
        }

        [HttpPost("updateExpense/{id}", Name = "UpdateExpense")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Expense expense)
        {
            if (id != expense.id)
            {
                return BadRequest("Expense ID mismatch");
            }
            if (expense == null)
            {
                return BadRequest("Expense cannot be null");
            }
            try
            {
                _context.Entry(expense).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.expenses.Any(e => e.id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost("removeExpense/{id}", Name = "DeleteExpense")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var expense = await _context.expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            try
            {
                _context.expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error deleting expense: {ex.Message}");
            }
            return NoContent();
        }

        [HttpGet("/health")]
        public IActionResult Health()
        {
            // Simple health check: verify database connectivity
            try
            {
                // Attempt to access the database
                _context.Database.CanConnect();
                return Ok(new { status = "Healthy" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { status = "Unhealthy" });
            }
        }
    }
}
