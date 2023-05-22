using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Shhmoney.ViewModels;
using Shhmoney.Models;

namespace Shhmoney.Views;

public partial class TransactionPopup : Popup
{
	private readonly Transaction transaction;
	private readonly TransactionViewModel transactionViewModel;

	public TransactionPopup(TransactionViewModel transactionViewModel, Transaction transaction)
	{
		InitializeComponent();
		BindingContext = transactionViewModel;
		this.transactionViewModel = transactionViewModel;
		this.transaction = transaction;
		if (transaction != null)
		{
            transactionViewModel.IsIncome = true;
            if (transaction is Income)
			{
				transactionViewModel.CurrentCategory = (transaction as Income).IncomeCategory;
            }
			else
			{
				transactionViewModel.IsIncome = false;
                transactionViewModel.CurrentCategory = (transaction as Expense).ExpenseCategory;
            }
			transactionViewModel.CurrentAccount = transaction.Account;
			transactionViewModel.Value = transaction.Value;
			transactionViewModel.Description = transaction.Description;

        }
	}

    void SaveButtonClicked(object sender, EventArgs e) => transactionViewModel.Save(this, e, transaction);

    void CancelButtonClicked(object sender, EventArgs e) => transactionViewModel.Cancel(this, e);
}