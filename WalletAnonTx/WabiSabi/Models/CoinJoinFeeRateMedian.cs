using NBitcoin;

namespace WalletAnonTx.WabiSabi.Models;

public record CoinJoinFeeRateMedian(TimeSpan TimeFrame, FeeRate MedianFeeRate);
