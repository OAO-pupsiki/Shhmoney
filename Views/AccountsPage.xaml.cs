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
    private void OnCurrencySelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        string selectedCurrencyCode = picker.SelectedItem as string;
        var accountViewModel = (AccountViewModel)BindingContext;
        accountViewModel.SelectedCurrency = accountViewModel.Currencies.FirstOrDefault(c => c.Code == selectedCurrencyCode);
    }

}