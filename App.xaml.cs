using Shhmoney.Utils;

namespace Shhmoney;

public partial class App : Application
{
	public App(CurrencyExchangeRate currencyExchangeRate)
	{
		InitializeComponent();
		MainPage = new AppShell(currencyExchangeRate);
	}
}
