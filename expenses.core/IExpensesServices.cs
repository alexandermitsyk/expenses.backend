using expenses.core.DTO;

namespace expenses.core
{
    public interface IExpensesServices
    {
        List<ExpenseDto> GetExpenses();

        ExpenseDto GetExpense(int id);

        ExpenseDto CreateExpense(db.Expense expense);

        void DeleteExpense(ExpenseDto expense);

        ExpenseDto EditExpense(ExpenseDto expense);
    }
}
