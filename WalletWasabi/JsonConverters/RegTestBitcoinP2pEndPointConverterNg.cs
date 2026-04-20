using WalletAnonTx.Helpers;

namespace WalletAnonTx.JsonConverters;

public class RegTestBitcoinP2pEndPointConverterNg : EndPointJsonConverterNg
{
	public RegTestBitcoinP2pEndPointConverterNg()
		: base(Constants.DefaultRegTestBitcoinCoreRpcPort)
	{
	}
}
