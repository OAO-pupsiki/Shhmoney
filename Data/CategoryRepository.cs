using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class CategoryRepository
    {
        private readonly DbContext _dbContext;

        public CategoryRepository()
        {
            _dbContext = new DbContext();
        }

        public void AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return _dbContext.Categories.SingleOrDefault(c => c.Id == id);
        }
    }
}
