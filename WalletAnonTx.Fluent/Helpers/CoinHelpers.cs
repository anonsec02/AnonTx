using WalletAnonTx.Blockchain.Analysis.Clustering;
using WalletAnonTx.Blockchain.TransactionOutputs;
using WalletAnonTx.Helpers;
using WalletAnonTx.Fluent.Models;
using WalletAnonTx.Blockchain.Transactions;

namespace WalletAnonTx.Fluent.Helpers;

public static class CoinHelpers
{
	public static LabelsArray GetLabels(this SmartCoin coin, int privateThreshold)
	{
		if (coin.IsPrivate(privateThreshold) || coin.IsSemiPrivate(privateThreshold))
		{
			return LabelsArray.Empty;
		}

		if (coin.HdPubKey.Cluster.Labels == LabelsArray.Empty)
		{
			return CoinPocketHelper.UnlabelledFundsText;
		}

		return coin.HdPubKey.Cluster.Labels;
	}

	public static int GetConfirmations(this SmartCoin coin) => coin.Transaction.GetConfirmations((int)Services.SmartHeaderChain.TipHeight);

	public static PrivacyLevel GetPrivacyLevel(this SmartCoin coin, int privateThreshold)
	{
		if (coin.IsPrivate(privateThreshold))
		{
			return PrivacyLevel.Private;
		}

		if (coin.IsSemiPrivate(privateThreshold))
		{
			return PrivacyLevel.SemiPrivate;
		}

		return PrivacyLevel.NonPrivate;
	}
}
