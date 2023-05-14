using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(CategoriesViewModel categoriesViewModel)
	{
		InitializeComponent();
        BindingContext = categoriesViewModel;

    }
    private void OnMainClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/main");
    }
    private void OnSettingsClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/settings");
    }
    private void OnAccountsClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/accounts");
    }
    private void OnArticlesClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/articles");
    }
}