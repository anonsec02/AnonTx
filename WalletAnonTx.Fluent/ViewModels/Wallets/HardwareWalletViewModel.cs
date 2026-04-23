using ReactiveUI;
using WalletAnonTx.Fluent.Extensions;
using WalletAnonTx.Fluent.Helpers;
using WalletAnonTx.Fluent.Models.UI;
using WalletAnonTx.Fluent.Models.Wallets;
using WalletAnonTx.Logging;
using WalletAnonTx.Wallets;

namespace WalletAnonTx.Fluent.ViewModels.Wallets;

public class HardwareWalletViewModel : WalletViewModel
{
	internal HardwareWalletViewModel(UiContext uiContext, IWalletModel walletModel, Wallet wallet) : base(uiContext, walletModel, wallet)
	{
		BroadcastPsbtCommand = ReactiveCommand.CreateFromTask(async () =>
		{
			try
			{
				var file = await FileDialogHelper.OpenFileAsync("Import Transaction", new[] { "psbt", "txn", "*" });
				if (file is { })
				{
					var path = file.Path.AbsolutePath;
					var txn = await walletModel.Transactions.LoadFromFileAsync(path);
					Navigate().To().BroadcastTransaction(txn);
				}
			}
			catch (Exception ex)
			{
				Logger.LogError(ex);
				await ShowErrorAsync(Title, ex.ToUserFriendlyString(), "It was not possible to load the transaction.");
			}
		});
	}
}
