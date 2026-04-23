using NBitcoin;
using WalletAnonTx.Blockchain.TransactionBuilding;
using WalletAnonTx.Blockchain.Transactions;

namespace WalletAnonTx.Fluent.Models;

public class TransactionAuthorizationInfo
{
	public TransactionAuthorizationInfo(BuildTransactionResult buildTransactionResult)
	{
		Psbt = buildTransactionResult.Psbt;
		Transaction = buildTransactionResult.Transaction;
	}

	public SmartTransaction Transaction { get; set; }

	public PSBT Psbt { get; }
}
