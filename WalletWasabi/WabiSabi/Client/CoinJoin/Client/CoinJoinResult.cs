using NBitcoin;
using System.Collections.Immutable;
using WalletAnonTx.Blockchain.TransactionOutputs;

namespace WalletAnonTx.WabiSabi.Client;

public abstract record CoinJoinResult;

public record SuccessfulCoinJoinResult(
	ImmutableList<SmartCoin> Coins,
	ImmutableList<Script> OutputScripts,
	Transaction UnsignedCoinJoin) : CoinJoinResult;

public record FailedCoinJoinResult : CoinJoinResult;

public record DisruptedCoinJoinResult(ImmutableList<SmartCoin> SignedCoins) : CoinJoinResult;
