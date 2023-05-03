﻿using System;
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
        private readonly LimitRepository _limitRepository;

        public LimitService()
        {
            _limitRepository = new LimitRepository();
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

       /* public void UpdateMounthLimit(int id, int limit, int totalLimit)
        {
            var mounthLimit = GetMounthLimitById(id);
            mounthLimit.Limit = limit;
            mounthLimit.TotalLimit = totalLimit;
            _limitRepository.Update(mounthLimit);
        }

        private MounthLimit GetMounthLimitById(int id)
        {
            return _limitRepository.GetMounthLimitById(id);
        }*/
    }

}