using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenses.db
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedDate { get; set; }

        public double Amount { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
