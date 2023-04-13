namespace Shhmoney.ViewModels;

public class CategoriesViewModel : ContentPage
{
	public CategoriesViewModel()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Start, Text = "Welcome to .NET MAUI2!"
				}
			}
		};
	}
}