using NBitcoin;

namespace WalletAnonTx.Blockchain.Transactions.Summary;

public class ForeignInput : IInput
{
	public Money? Amount => default;
}
