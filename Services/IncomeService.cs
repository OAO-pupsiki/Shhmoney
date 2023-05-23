using Shhmoney.Data;
using Shhmoney.Models;

namespace Shhmoney.Services
{
    internal class IncomeService
    {
        private readonly IncomeRepository _incomeRepository;

        public IncomeService(IncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public void AddIncome(Income income)
        {
            _incomeRepository.AddIncome(income);
        }

        public void DelIncomeByCategory(Category category)
        {
             _incomeRepository.DeleteIncomeByUserId(category.UserId);
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
