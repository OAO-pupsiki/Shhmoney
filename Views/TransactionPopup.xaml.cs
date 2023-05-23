using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Shhmoney.ViewModels;

namespace Shhmoney.Views;

public partial class TransactionPopup : Popup
{
	private readonly TransactionViewModel transactionViewModel;

	public TransactionPopup(TransactionViewModel transactionViewModel)
	{
		InitializeComponent();
		BindingContext = transactionViewModel;
		this.transactionViewModel = transactionViewModel;
	}

    void SaveButtonClicked(object sender, EventArgs e) => transactionViewModel.Save(this, e);

    void CancelButtonClicked(object sender, EventArgs e) => transactionViewModel.Cancel(this, e);
}