using DynamicData;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WalletAnonTx.Blockchain.TransactionProcessing;
using WalletAnonTx.Fluent.Extensions;
using WalletAnonTx.Fluent.Helpers;
using WalletAnonTx.Fluent.Infrastructure;
using WalletAnonTx.Fluent.Models.Wallets;
using WalletAnonTx.Fluent.ViewModels.Navigation;

namespace WalletAnonTx.Fluent.ViewModels.Wallets.Notifications;

[AppLifetime]
public partial class WalletNotificationsViewModel : ViewModelBase
{
	private readonly IWalletSelector _walletSelector;
	[AutoNotify] private bool _isBusy;

	private WalletNotificationsViewModel(IWalletSelector walletSelector)
	{
		_walletSelector = walletSelector;
	}

	public void StartListening()
	{
		UiContext.WalletRepository.Wallets
			.Connect()
			.AutoRefresh(x => x.IsLoggedIn)
			.Filter(x => x.IsLoggedIn)
			.FilterOnObservable(x => x.State.Select(s => s == WalletAnonTx.Wallets.WalletState.Started))
			.MergeMany(x => x.Transactions.NewTransactionArrived)
			.Where(x => !UiContext.ApplicationSettings.PrivacyMode)
			.Where(x => x.EventArgs.IsNews)
			.DoAsync(x => OnNotificationReceivedAsync(x.Wallet, x.EventArgs))
			.Subscribe();
	}

	private async Task OnNotificationReceivedAsync(IWalletModel wallet, ProcessedResult e)
	{
		if (!e.IsOwnCoinJoin)
		{
			void OnClick()
			{
				if (UiContext.Navigate().IsAnyPageBusy)
				{
					return;
				}

				var wvm = _walletSelector.To(wallet);
				wvm?.SelectTransaction(e.Transaction.GetHash());
			}

			NotificationHelpers.Show(wallet, e, OnClick);
		}

		if (_walletSelector.SelectedWalletModel == wallet && (e.NewlyReceivedCoins.Count != 0 || e.NewlyConfirmedReceivedCoins.Count != 0))
		{
			await Task.Delay(200);
			_walletSelector.SelectedWallet?.SelectTransaction(e.Transaction.GetHash());
		}
	}
}
