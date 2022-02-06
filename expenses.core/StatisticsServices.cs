using Microsoft.AspNetCore.Http;

namespace expenses.core
{
    public class StatisticsServices : IStatisticsServices
    {
        private readonly db.AppDbContext _context;
        private readonly db.User _user;

        public StatisticsServices(db.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public IEnumerable<KeyValuePair<string, double>> GetExpenseAmountPerCategory() =>
            _context.Expenses
                .Where(e => e.User.Id == _user.Id)
                .AsEnumerable()
                .GroupBy(e => e.Description)
                .ToDictionary(e => e.Key, e => e.Sum(x => x.Amount))
                .Select(x => new KeyValuePair<string, double>(x.Key, x.Value));
    }
}
