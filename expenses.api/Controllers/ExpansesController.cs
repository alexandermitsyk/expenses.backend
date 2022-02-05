using expenses.core;
using expenses.db;
using expenses.core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenses.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExpansesController : ControllerBase
    {
        private IExpensesServices _services;

        public ExpansesController(IExpensesServices services)
        {
            _services = services;
        }
        
        [HttpGet] 
        public IActionResult GetExpenses()
        {
            return Ok(_services.GetExpenses());
        }

        [HttpGet("{id}", Name = "GetExpense")]
        public IActionResult GetExpense(int id)
        {
            return Ok(_services.GetExpense(id));
        }

        [HttpPost]
        public IActionResult CreateExpanse(Expense expense)
        {
            var newExpense = _services.CreateExpense(expense);
            return CreatedAtRoute("GetExpense", new { newExpense.Id }, newExpense);
        }

        [HttpDelete]
        public IActionResult DeleteExpense(ExpenseDto expense)
        {
            _services.DeleteExpense(expense);

            return Ok();
        }

        [HttpPut]
        public IActionResult EditExpanse(ExpenseDto expense)
        {
            return Ok(_services.EditExpense(expense));
        }
    }
}