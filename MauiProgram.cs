using Microsoft.Extensions.Logging;
using Shhmoney.Data;
using Shhmoney.ViewModels;
using Shhmoney.Services;
using Shhmoney.Views;
using Shhmoney.Utils;

namespace Shhmoney;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<DbContext>();

        builder.Services.AddSingleton<AccountRepository>();
        builder.Services.AddSingleton<CurrencyRepository>();
        builder.Services.AddSingleton<ExpenseCategoryRepository>();
        builder.Services.AddSingleton<ExpenseRepository>();
        builder.Services.AddSingleton<IncomeCategoryRepository>();
        builder.Services.AddSingleton<IncomeRepository>();
        builder.Services.AddSingleton<RoleRepository>();
        builder.Services.AddSingleton<UserRepository>();
        builder.Services.AddSingleton<UserSessionRepository>();
        builder.Services.AddSingleton<LimitRepository>();

		builder.Services.AddSingleton<CurrencyExchangeRate>();

        builder.Services.AddSingleton<AuthenticationService>();

		builder.Services.AddSingleton<AuthenticationViewModel>();
		builder.Services.AddSingleton<AuthenticationPage>();

		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<LoginPage>();

		builder.Services.AddSingleton<SignUpViewModel>();
		builder.Services.AddSingleton<SignUpPage>();

		builder.Services.AddSingleton<UserService>();
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddSingleton<CategoriesViewModel>();
        builder.Services.AddSingleton<CategoriesPage>();

        builder.Services.AddSingleton<LimitService>();
        builder.Services.AddSingleton<LimitViewModel>();
        builder.Services.AddSingleton<LimitsPage>();

        builder.Services.AddSingleton<AccountService>();
        builder.Services.AddSingleton<AccountViewModel>();
        builder.Services.AddSingleton<AccountsPage>();

        builder.Services.AddSingleton<ArticlesPage>();

        builder.Services.AddSingleton<TransactionService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
