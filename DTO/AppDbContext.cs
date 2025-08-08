using ExpensesBE.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesBE.DTO
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Expense> expenses { get; set; }
    }
}
