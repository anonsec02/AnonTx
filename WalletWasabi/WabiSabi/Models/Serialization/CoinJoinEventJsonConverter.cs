using WalletAnonTx.WabiSabi.Models.MultipartyTransaction;

namespace WalletAnonTx.WabiSabi.Models.Serialization;

public class CoinJoinEventJsonConverter : GenericInterfaceJsonConverter<IEvent>
{
	public CoinJoinEventJsonConverter() : base(new[] { typeof(InputAdded), typeof(OutputAdded), typeof(RoundCreated) })
	{
	}
}
