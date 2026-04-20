using WalletAnonTx.WabiSabi.Models.MultipartyTransaction;

namespace WalletAnonTx.WabiSabi.Models.Serialization;

public class MultipartyTransactionStateJsonConverter : GenericInterfaceJsonConverter<MultipartyTransactionState>
{
	public MultipartyTransactionStateJsonConverter() : base(new[] { typeof(ConstructionState), typeof(SigningState) })
	{
	}
}
