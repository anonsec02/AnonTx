using System.Collections.Generic;

namespace WalletAnonTx.Affiliation.Models.CoinJoinNotification;

public record Body(
	string TransactionId,
	IEnumerable<Input> Inputs,
	IEnumerable<Output> Outputs,
	long Slip44CoinType,
	CoordinatorFeeRate FeeRate,
	long NoFeeThreshold,
	long MinRegistrableAmount,
	long Timestamp);
