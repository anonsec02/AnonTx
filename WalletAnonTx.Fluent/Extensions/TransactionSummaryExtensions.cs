using WalletAnonTx.Blockchain.Transactions;
using WalletAnonTx.Fluent.Helpers;

namespace WalletAnonTx.Fluent.Extensions;

public static class TransactionSummaryExtensions
{
	public static bool IsConfirmed(this TransactionSummary model)
	{
		var confirmations = model.GetConfirmations();
		return confirmations > 0;
	}

	public static int GetConfirmations(this TransactionSummary model)
		=> model.Transaction.GetConfirmations((int)Services.SmartHeaderChain.ServerTipHeight);
}
