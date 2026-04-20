using WalletAnonTx.Helpers;

namespace WalletAnonTx.JsonConverters;

public class TestNetBitcoinP2pEndPointConverterNg : EndPointJsonConverterNg
{
	public TestNetBitcoinP2pEndPointConverterNg()
		: base(Constants.DefaultTestNetBitcoinP2pPort)
	{
	}
}
