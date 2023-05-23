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
}