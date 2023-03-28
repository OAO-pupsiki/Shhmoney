namespace Shhmoney.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Income> Incomes { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
