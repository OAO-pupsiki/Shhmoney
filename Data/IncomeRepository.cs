﻿using Shhmoney.Models;
using Microsoft.EntityFrameworkCore;

namespace Shhmoney.Data
{
    public class IncomeRepository
    {
        private readonly DbContext _dbContext;

        public IncomeRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddIncome(Income income)
        {
            _dbContext.Incomes.Add(income);
            _dbContext.SaveChanges();
        }

        public void RemoveIncome(Income income)
        {
            _dbContext.Incomes.Remove(income);
            _dbContext.SaveChanges();
        }

        public void UpdateIncome(Income income) 
        {
            //_dbContext.Incomes.Update(income);
            _dbContext.SaveChanges();
        }

        public void DeleteIncomeByUserId(int userId)
        {
            var list = _dbContext.Incomes.Where(e => e.UserId == userId).ToList();

            _dbContext.Incomes.RemoveRange(list);
            _dbContext.SaveChanges();
        }

        public Income GetIncomeById(int id)
        {
            return _dbContext.Incomes.SingleOrDefault(i => i.Id == id);
        }

        public List<Income> GetIncomesByAccount(Account account)
        {
            return _dbContext.Incomes.Where(i =>  i.Account == account).ToList();
        }

        public List<Income> GetIncomesByUser(User user)
        {
            return _dbContext.Incomes
                .Include(i => i.Account)
                .Include(i => i.IncomeCategory)
                .Where(i => i.UserId == user.Id)
                .ToList();
        }

        public List<Income> GetIncomesByCategory(Category category)
        {
            return _dbContext.Incomes.Where(i => i.IncomeCategory == category).ToList();
        }
    }
}
