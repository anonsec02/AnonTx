using NBitcoin;

namespace WalletAnonTx.Blockchain.Transactions.Summary;

public class KnownInput : IInput
{
	public KnownInput(Money amount)
	{
		Amount = amount;
	}

	public virtual Money? Amount { get; }
}
