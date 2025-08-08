using System.ComponentModel;

namespace ExpensesBE.Models
{
    public class Expense
    {
        public int id { get; set; }
        public required string purchase { get; set; }
        public required DateOnly? purchasedate { get; set; } = DateOnly.MinValue;
        public required DateOnly? timestamp{ get; set; } = DateOnly.MinValue;
        public required string category { get; set; }
        public required int amount { get; set; }
    }
}
