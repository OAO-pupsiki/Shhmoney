using Shhmoney.ViewModels;

namespace Shhmoney.Views;

public partial class TransactionPage : ContentPage
{
	public TransactionPage(TransactionViewModel transactionViewModel)
	{
		InitializeComponent();
		BindingContext = transactionViewModel;
	}
}