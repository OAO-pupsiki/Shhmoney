using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class UserSessionRepository
    {
        private readonly DbContext _dbContext;

        public UserSessionRepository()
        {
            _dbContext = DbContext.GetDbContext();
        }

        public void AddSession(UserSession session)
        {
            _dbContext.Sessions.Add(session);
            _dbContext.SaveChanges();
        }

        public void UpdateSession(UserSession session) 
        {
            session.Expiration = session.Expiration.AddDays(30);
            _dbContext.SaveChanges();
        }

        public UserSession GetSessionByToken(string token)
        {
            return _dbContext.Sessions.SingleOrDefault(s => s.Token == token);
        }

        public UserSession GetSessionByUser(User user)
        {
            return _dbContext.Sessions.SingleOrDefault(s => s.User == user);
        }

        public void RemoveSessionByToken(string token)
        {
            _dbContext.Sessions.Remove(GetSessionByToken(token));
            _dbContext.SaveChanges();
        }
    }
}
