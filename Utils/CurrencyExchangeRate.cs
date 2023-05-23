using Shhmoney.Models;
using Shhmoney.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Shhmoney.Utils
{
    public class CurrencyExchangeRate
    {
        private readonly CurrencyRepository _currencyRepository;

        public CurrencyExchangeRate(CurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<string> GetApiResponse()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(" https://www.nbrb.by/api/exrates/rates?periodicity=0");
            return await response.Content.ReadAsStringAsync();
        }

        public async void LoadCurrencies()
        {
            var apiResponse = await GetApiResponse();

            if (string.IsNullOrEmpty(apiResponse))
                throw new Exception("Unable to load currencies");

            var jsonResponse = JArray.Parse(apiResponse);

            foreach (var obj in jsonResponse)
            {
                var code = (string)obj["Cur_Abbreviation"];
                if (_currencyRepository.GetCurrency(code) == null)
                {
                    var currency = new Currency
                    {
                        Code = code,
                        Value = (decimal)obj["Cur_OfficialRate"]
                    };
                    _currencyRepository.AddCurrency(currency);
                }
            }
        }

        public async void UpdateCurrencies()
        {
            var apiResponse = await GetApiResponse();

            if (string.IsNullOrEmpty(apiResponse))
                throw new Exception("Unable to load currencies");

            var jsonResponse = JArray.Parse(apiResponse);

            foreach(var obj in jsonResponse)
            {
                var code = (string)obj["Cur_Abbreviation"];
                var currency = _currencyRepository.GetCurrency(code);

                if(currency == null)
                {
                    currency = new Currency
                    {
                        Code = code,
                        Value = (decimal)obj["Cur_OfficialRate"]
                    };
                }

                currency.Value = Convert.ToDecimal((string)obj["Cur_OfficialRate"]);
                _currencyRepository.UpdateCurrency(currency);
            }
        }
    }
}
