using NBitcoin;

namespace WalletAnonTx.Affiliation.Models.CoinJoinNotification;

public record Outpoint(byte[] Hash, long Index)
{
	public static Outpoint FromOutPoint(OutPoint outPoint) =>
		new(outPoint.Hash.ToBytes(), outPoint.N);
}
