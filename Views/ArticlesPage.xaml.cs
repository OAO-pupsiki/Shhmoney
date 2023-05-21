using Shhmoney.ViewModels;
namespace Shhmoney.Views;
public partial class ArticlesPage : ContentPage
{
	public ArticlesPage()
	{
		InitializeComponent();
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
    private void OnAccountsClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/accounts");
    }
    private async void OnLinkTapped(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("http://fingramota.by/?ysclid=lhxktfxin0198682388"));
    }
    private async void OnLinkTapped2(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://www.nbrb.by/statistics/rates/ratesdaily.asp"));
    }
    private async void OnLinkTapped3(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("http://fingramota.by/ru/guide/practical"));
    }
    private async void OnLinkTapped4(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://profi-investor.com/investment-advisor/kak-zarabotat-online/10-saitov-dlya-povishenia-fin-gramotnosti?ysclid=lhxlhxfvyv531268613"));
    }
    private async void OnLinkTapped5(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://vc.ru/education/658895-15-istochnikov-knigi-prilozheniya-i-telegram-kanaly-o-finansovoy-gramotnosti-v-biznese-i-v-zhizni?ysclid=lhxlpnlu6725639339"));
    }
    private async void OnImageTapped(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://moneymuseum.by/ru/"));
    }

}