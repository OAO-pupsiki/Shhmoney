using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage()
	{
		InitializeComponent();
        BindingContext = new CategoriesViewModel();

    }
}