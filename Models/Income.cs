namespace Shhmoney.Models
{
    public class Income
    {
        private decimal value;

        public int Id { get; set; }
        public string Currency { get; set; }
        public DateTime DateTime { get; set; }

        public int IncomeCategoryId { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
