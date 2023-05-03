using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shhmoney.Models;

namespace Shhmoney.Data
{
    class LimitRepository
    {       
        private readonly DbContext _dbContext;
        public LimitRepository() 
        {
            _dbContext = DbContext.GetDbContext();
        }
        public MounthLimit Add(MounthLimit limit)
        {
            var dbItem = _dbContext.MounthLimits.Add(limit);
            _dbContext.SaveChanges();
            return dbItem.Entity;
        }
        public void Update(MounthLimit item)
        {
            _dbContext.MounthLimits.Update(item);
            _dbContext.SaveChanges();
        }

    }
}
