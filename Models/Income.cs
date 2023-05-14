namespace Shhmoney.Models
{
    public class Income : Transaction
    {
        public int IncomeCategoryId { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
    }
}
