using WalletAnonTx.Wallets;

namespace WalletAnonTx.WabiSabi.Client.StatusChangedEvents;

public class LoadedEventArgs : StatusChangedEventArgs
{
	public LoadedEventArgs(IWallet wallet)
		: base(wallet)
	{
	}
}
