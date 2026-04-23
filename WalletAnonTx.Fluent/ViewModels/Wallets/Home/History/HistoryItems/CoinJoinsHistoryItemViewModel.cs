using ReactiveUI;
using WalletAnonTx.Fluent.Models.UI;
using WalletAnonTx.Fluent.Models.Wallets;

namespace WalletAnonTx.Fluent.ViewModels.Wallets.Home.History.HistoryItems;

public partial class CoinJoinsHistoryItemViewModel : HistoryItemViewModelBase
{
	private CoinJoinsHistoryItemViewModel(IWalletModel wallet, TransactionModel transaction) : base(transaction)
	{
		ShowDetailsCommand = ReactiveCommand.Create(() => UiContext.Navigate().To().CoinJoinsDetails(wallet, transaction));
	}
}
