using System.ComponentModel;

namespace ExpensesBE.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public required string Purchase { get; set; }
        public required DateOnly? PurchaseDate { get; set; } = DateOnly.MinValue;
        public required DateOnly? TimeStamp{ get; set; } = DateOnly.MinValue;
        public required string Category { get; set; }
        public required int Amount { get; set; }
    }
}
