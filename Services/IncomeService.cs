using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shhmoney.Data;
using Shhmoney.Models;

namespace Shhmoney.Services
{
    internal class IncomeService
    {
        private readonly IncomeRepository _incomeRepository;

        public IncomeService()
        {
            _incomeRepository = new IncomeRepository();
        }

        public void AddIncome(Income income)
        {
            _incomeRepository.AddIncome(income);
        }

        public void DelIncomeByCategory(Category category)
        {
            int userId = 1; // здесь должен браться id текущего пользователя
            var lstExp = _incomeRepository.GetIncomesByCategory(category);
            foreach (var exp in lstExp.Where(exp => exp.Account.UserId == userId))
            {
                _incomeRepository.DeleteIncome(exp.Account.UserId);
            }
        }

        public List<Income> ViewIncomeFromCategory(Category category)
        {
            return _incomeRepository.GetIncomesByCategory(category).ToList();
        }

        public void ChangeIncome(Category category, Income income)
        {

        }
    }
}
