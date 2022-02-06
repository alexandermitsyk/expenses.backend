using expenses.core.DTO;
using Microsoft.AspNetCore.Http;

namespace expenses.core
{
    public class ExpensesServices : IExpensesServices
    {
        private db.AppDbContext _context;
        private readonly db.User _user;
        public ExpensesServices(db.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user =  _context.Users.First(u => u.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public List<ExpenseDto> GetExpenses()
        {
            return _context.Expenses
             .Where(x => x.User.Id == _user.Id)
             .OrderByDescending(x => x.Id)
             .Select(e => (ExpenseDto)e)
             .ToList();
        }

        public ExpenseDto GetExpense(int id)
        {
            return _context.Expenses
                .Where(x => x.User.Id == _user.Id && x.Id == id)
                .Select(e => (ExpenseDto)e).First();
        }

        public ExpenseDto CreateExpense(db.Expense expense)
        {
            expense.User = _user;
            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return (ExpenseDto)expense;
        }

        public void DeleteExpense(ExpenseDto expense)
        {
            var dbExpense = _context.Expenses.First(e => e.User.Id == _user.Id && e.Id == expense.Id);
            _context.Remove(dbExpense);
            _context.SaveChanges();
        }

        public ExpenseDto EditExpense(ExpenseDto expense)
        {
            var dbExpense = _context.Expenses.First(x => x.User.Id == _user.Id && x.Id == expense.Id);

            dbExpense.Description = expense.Description;
            dbExpense.Amount = expense.Amount;
            dbExpense.Comment = expense.Comment;
            dbExpense.CreatedDate = expense.CreatedDate;

            _context.SaveChanges();

            return expense;
        }
    }
}