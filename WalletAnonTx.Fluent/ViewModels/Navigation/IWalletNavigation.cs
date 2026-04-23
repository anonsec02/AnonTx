using WalletAnonTx.Fluent.Models.Wallets;
using WalletAnonTx.Fluent.ViewModels.Wallets;

namespace WalletAnonTx.Fluent.ViewModels.Navigation;

public interface IWalletNavigation
{
	IWalletViewModel? To(IWalletModel wallet);
}

public interface IWalletSelector : IWalletNavigation
{
	IWalletViewModel? SelectedWallet { get; }

	IWalletModel? SelectedWalletModel { get; }
}
