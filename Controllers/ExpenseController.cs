using ExpensesBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : Controller
    {
        [HttpGet(Name = "GetExpenses")]
        public List<Expense> Get()
        {
            var expenses= new List<Expense>
            {
                new Expense
                {
                    Id = 1,
                    Purchase = "Coffee",
                    PurchaseDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                    TimeStamp = DateOnly.FromDateTime(DateTime.Now),
                    Category = "Food",
                    Amount = 5
                },
                new Expense
                {
                    Id = 2,
                    Purchase = "Lunch",
                    PurchaseDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                    TimeStamp = DateOnly.FromDateTime(DateTime.Now),
                    Category = "Food",
                    Amount = 15
                }
            };
            return expenses;
        }

        [HttpPost(Name = "AddExpense")]
        public IActionResult Post([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("Expense cannot be null");
            }
            // Here you would typically save the expense to a database
            // For this example, we will just return it as if it was saved successfully
            return CreatedAtAction(nameof(Get), new { id = expense.Id }, expense);
        }
}
