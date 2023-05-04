namespace Shhmoney;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        MoneyTypes.SelectedIndex = 0;
    }

    void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        //DisplayAlert("Вид денежных средств", $"Вы выбрали: {MoneyTypes.SelectedItem}", "Oк");
    }
    private void OnCategoriesClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/categories");
    }
    private void OnSettingsClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/settings");
    }
}