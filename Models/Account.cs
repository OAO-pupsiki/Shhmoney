namespace Shhmoney.Models
{
    public class Account
    {
        private decimal balance;

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Income> Incomes { get; set; }
        public List<Expense> Expenses { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
