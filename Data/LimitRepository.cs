using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class LimitRepository
    {       
        private readonly DbContext dbContext;
        public LimitRepository(DbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public MounthLimit Add(MounthLimit limit)
        {
            var dbLimit = dbContext.MounthLimits.FirstOrDefault(c => c.ExpenseCategoryId == limit.ExpenseCategoryId);
            if (dbLimit == null)
            {
                var dbItem = dbContext.MounthLimits.Add(limit);
                dbContext.SaveChanges();
                return dbItem.Entity;
            }
            else
            {
                dbLimit.Limit = limit.Limit;
                dbContext.MounthLimits.Update(dbLimit);
                dbContext.SaveChanges();
                return dbLimit;
            }
        }

        public MounthLimit GetMounthLimitByCategoryId(int categoryId)
        {
            return dbContext.MounthLimits.FirstOrDefault(c => c.ExpenseCategoryId == categoryId);
        }

    }
}
