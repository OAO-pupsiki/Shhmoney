using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shhmoney.Data;
using Shhmoney.Models;

namespace Shhmoney.Services
{
    public class LimitService
    {
        private LimitRepository _limitRepository;

        public LimitService(LimitRepository limitRepository)
        {
            _limitRepository = limitRepository;
        }
        public MounthLimit Add(int categoryId, string currency, int limit)
        {
            var mounthLimit = new MounthLimit
            {
                UserId = Utils.AppContext.CurrentUser.Id,
                ExpenseCategoryId = categoryId,
                Currency = currency,
                Limit = limit,
            };
            return _limitRepository.Add(mounthLimit);
        }
        public bool IsExceeded(int categoryId, decimal expenseAmount)
        {
            var monthLimit = _limitRepository.GetMounthLimitByCategoryId(categoryId);

            // Проверяем, если месячный лимит не найден, считаем, что он не превышен
            if (monthLimit == null)
            {
                return false;
            }

            // Сравниваем расход с лимитом
            return expenseAmount > monthLimit.Limit;
        }
    





        /* public void UpdateMounthLimit(int id, int limit, int totalLimit)
         {
             var mounthLimit = GetMounthLimitById(id);
             mounthLimit.Limit = limit;
             mounthLimit.TotalLimit = totalLimit;
             _limitRepository.Update(mounthLimit);
         }*/

        public MounthLimit GetMounthLimitByCategoryId(int categoryId)
        {
            return _limitRepository.GetMounthLimitByCategoryId(categoryId);
        }
    
    }

}
