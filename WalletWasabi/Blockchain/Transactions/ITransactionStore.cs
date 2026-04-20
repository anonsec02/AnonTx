using System.Diagnostics.CodeAnalysis;
using NBitcoin;

namespace WalletAnonTx.Blockchain.Transactions;

public interface ITransactionStore
{
	bool TryGetTransaction(uint256 hash, [NotNullWhen(true)] out SmartTransaction? sameStx);
}
