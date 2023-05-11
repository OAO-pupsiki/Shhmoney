namespace Shhmoney.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
