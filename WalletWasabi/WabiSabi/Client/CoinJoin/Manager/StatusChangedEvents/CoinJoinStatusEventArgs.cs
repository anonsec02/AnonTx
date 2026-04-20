using WalletAnonTx.WabiSabi.Client.CoinJoinProgressEvents;
using WalletAnonTx.Wallets;

namespace WalletAnonTx.WabiSabi.Client.StatusChangedEvents;

public class CoinJoinStatusEventArgs : StatusChangedEventArgs
{
	public CoinJoinStatusEventArgs(IWallet wallet, CoinJoinProgressEventArgs coinJoinProgressEventArgs) : base(wallet)
	{
		CoinJoinProgressEventArgs = coinJoinProgressEventArgs;
	}

	public CoinJoinProgressEventArgs CoinJoinProgressEventArgs { get; }
}
