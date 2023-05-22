namespace Shhmoney.Models
{
    public abstract class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public bool IsBased { get; set; }
        public User User { get; set; }
    }
}
