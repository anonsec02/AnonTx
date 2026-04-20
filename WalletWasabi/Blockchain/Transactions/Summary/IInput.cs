using NBitcoin;

namespace WalletAnonTx.Blockchain.Transactions.Summary;

public interface IInput
{
	Money? Amount { get; }
}
