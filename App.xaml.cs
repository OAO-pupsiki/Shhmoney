using Shhmoney.Services;
using Shhmoney.Utils;

namespace Shhmoney;

public partial class App : Application
{
	public App(CurrencyExchangeRate currencyExchangeRate, AuthenticationService authenticationServices)
	{
		InitializeComponent();
		MainPage = new AppShell(currencyExchangeRate, authenticationServices);
	}
}
