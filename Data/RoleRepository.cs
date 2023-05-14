using Microsoft.EntityFrameworkCore;
using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class RoleRepository
    {
        private readonly DbContext _dbContext;

        public RoleRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRole(Role role)
        {
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();
        }

        public Role GetRoleById(int id)
        {
            return _dbContext.Roles.FirstOrDefault(r => r.Id == id);
        }

        public Role GetRoleByName(string roleName)
        {
            return _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
        }

        public List<Role> GetAllRoles()
        {
            return _dbContext.Roles.ToList();
        }
    }
}
