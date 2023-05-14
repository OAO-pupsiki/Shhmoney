using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class LimitsPage : ContentPage
{
    public LimitsPage(LimitViewModel limitViewModel)
    {
        InitializeComponent();
        BindingContext = limitViewModel;
        CurrencyTypes.SelectedIndex = 0;
    }
    void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        //DisplayAlert("Вид денежных средств", $"Вы выбрали: {MoneyTypes.SelectedItem}", "Oк");
    }
    private void OnMainClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/main");
    }
    private void OnCategoriesClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/categories");
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
