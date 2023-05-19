using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class AccountsPage : ContentPage
{
    public AccountsPage(AccountViewModel accountViewModel)
	{
		InitializeComponent();
        BindingContext = accountViewModel;
    }
    private void OnMainClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/main");
    }
    private void OnCategoriesClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/categories");
    }
    private void OnSettingsClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/settings");
    }
    private void OnArticlesClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/articles");
    }
}