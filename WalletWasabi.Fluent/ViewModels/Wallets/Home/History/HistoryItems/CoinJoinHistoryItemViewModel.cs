using ReactiveUI;
using WalletAnonTx.Fluent.Models.UI;
using WalletAnonTx.Fluent.Models.Wallets;

namespace WalletAnonTx.Fluent.ViewModels.Wallets.Home.History.HistoryItems;

public partial class CoinJoinHistoryItemViewModel : HistoryItemViewModelBase
{
	private CoinJoinHistoryItemViewModel(IWalletModel wallet, TransactionModel transaction) : base(transaction)
	{
		ShowDetailsCommand = ReactiveCommand.Create(() => UiContext.Navigate().To().CoinJoinDetails(wallet, transaction));
	}
}
