namespace Shhmoney.Models
{
    public class Expense
    {
        private decimal value;

        public int Id { get; set; }
        public string Currency { get; set; }
        public DateTime DateTime { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
