using CommunityToolkit.Maui.Views;
using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class MainPage : ContentPage
{
    public MainViewModel _mainViewModel { get; set; }

    public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
        _mainViewModel = mainViewModel; 

    }
    private void OnCategoriesClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/categories");
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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _mainViewModel.UpdateList();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        bool res = await DisplayAlert("Подтвердить действие", "Вы действительно хотите удалить транзакцию?", "Да", "Нет");
        if (res)
        {
            _mainViewModel.RemoveTransaction();
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        _mainViewModel.ShowTransactionInfo();
    }
}