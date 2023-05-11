using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}
}