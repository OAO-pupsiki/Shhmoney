using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class UserSessionRepository
    {
        private readonly DbContext _dbContext;

        public UserSessionRepository()
        {
            _dbContext = new DbContext();
        }

        public void AddSession(UserSession session)
        {
            _dbContext.Sessions.Add(session);
        }

        public void UpdateSession(UserSession session) 
        {
            session.Expiration.AddDays(30);
        }

        public UserSession GetSessionByToken(string token)
        {
            return _dbContext.Sessions.SingleOrDefault(s => s.Token == token);
        }

        public void RemoveSessionByToken(string token)
        {
            _dbContext.Sessions.Remove(GetSessionByToken(token));
        }
    }
}
