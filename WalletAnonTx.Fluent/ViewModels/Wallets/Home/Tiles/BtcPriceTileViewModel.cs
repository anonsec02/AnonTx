using WalletAnonTx.Fluent.Models.Wallets;

namespace WalletAnonTx.Fluent.ViewModels.Wallets.Home.Tiles;

public class BtcPriceTileViewModel : ActivatableViewModel
{
	public BtcPriceTileViewModel(IAmountProvider amountProvider)
	{
		UsdPerBtc = amountProvider.BtcToUsdExchangeRates;
	}

	public IObservable<decimal> UsdPerBtc { get; }
}
