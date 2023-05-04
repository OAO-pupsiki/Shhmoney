using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class LimitsPage : ContentPage
{
    public LimitsPage()
    {
        InitializeComponent();
        BindingContext = new LimitViewModel();
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
}
