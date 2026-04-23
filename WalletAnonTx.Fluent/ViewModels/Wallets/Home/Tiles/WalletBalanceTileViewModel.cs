using WalletAnonTx.Fluent.Models.Wallets;

namespace WalletAnonTx.Fluent.ViewModels.Wallets.Home.Tiles;

public class WalletBalanceTileViewModel : ActivatableViewModel
{
	public WalletBalanceTileViewModel(IObservable<Amount> amounts)
	{
		Amounts = amounts;
	}

	public IObservable<Amount> Amounts { get; }
}
