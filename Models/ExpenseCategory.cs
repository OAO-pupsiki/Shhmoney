namespace Shhmoney.Models
{
    public class ExpenseCategory : Category
    {
        public List<Expense> Expenses { get; set; }
    }
}
