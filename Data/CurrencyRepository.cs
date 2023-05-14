using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class CurrencyRepository
    {
        private readonly DbContext _dbContext;

        public CurrencyRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCurrency(Currency currency)
        {
            _dbContext.Currencies.Add(currency);
            _dbContext.SaveChanges();
        }

        public void UpdateCurrency(Currency currency)
        {
            _dbContext.Update(currency);
            _dbContext.SaveChanges();
        }

        public Currency GetCurrency(string code)
        {
            return _dbContext.Currencies.SingleOrDefault(c => c.Code == code);
        }

        public List<Currency> GetAllCurrencies()
        {
            return _dbContext.Currencies.ToList();
        }
    }
}
