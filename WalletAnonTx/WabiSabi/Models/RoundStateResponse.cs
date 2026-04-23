using WalletAnonTx.Affiliation.Models;
namespace WalletAnonTx.WabiSabi.Models;

public record RoundStateResponse(RoundState[] RoundStates, CoinJoinFeeRateMedian[] CoinJoinFeeRateMedians, AffiliateInformation AffiliateInformation);
