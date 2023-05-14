using Shhmoney.Services;
using Shhmoney.Utils;

namespace Shhmoney;

public partial class AppShell : Shell
{
	public AppShell(CurrencyExchangeRate currencyExchangeRate)
	{
		InitializeComponent();
        currencyExchangeRate.LoadCurrencies();
	} 
}