namespace Shhmoney.Models
{
    public class Expense : Transaction
    {
        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
