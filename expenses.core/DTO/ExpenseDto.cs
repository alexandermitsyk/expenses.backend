namespace expenses.core.DTO
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public double Amount { get; set; }

        public static explicit operator ExpenseDto(db.Expense e) => new ExpenseDto
        {
            Id = e.Id,
            Amount = e.Amount,
            Description = e.Description,
        };
    }
}
