using NBitcoin;

namespace WalletAnonTx.Fluent.ViewModels.Wallets;

public interface IWalletViewModel
{
	void SelectTransaction(uint256 txid);
}
