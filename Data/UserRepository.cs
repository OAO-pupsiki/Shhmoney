using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class UserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }
    }
}
