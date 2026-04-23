using NBitcoin;

namespace WalletAnonTx.WabiSabi.Backend.Banning;

public record CoinVerifyResult(Coin Coin, bool ShouldBan, bool ShouldRemove);
