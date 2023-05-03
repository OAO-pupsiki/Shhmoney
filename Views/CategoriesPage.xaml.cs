using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage()
	{
		InitializeComponent();
        BindingContext = new CategoriesViewModel();

    }
    private void OnButtonClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/main");
    }
}