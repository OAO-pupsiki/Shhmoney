using Shhmoney.Services;
using Shhmoney.Utils;

namespace Shhmoney;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		var a = new CurrencyExchangeRate();
		a.LoadCurrencies();
	} 
}